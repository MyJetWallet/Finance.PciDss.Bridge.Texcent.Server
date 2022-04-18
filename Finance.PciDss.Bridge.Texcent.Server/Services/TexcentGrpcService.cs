using System;
using System.Threading.Tasks;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.Texcent.Server.Services.Extensions;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations;
using Finance.PciDss.PciDssBridgeGrpc;
using Finance.PciDss.PciDssBridgeGrpc.Contracts;
using Finance.PciDss.PciDssBridgeGrpc.Contracts.Enums;
using MyCrm.AuditLog.Grpc;
using MyCrm.AuditLog.Grpc.Models;
using Newtonsoft.Json;
using Serilog;
using SimpleTrading.GrpcTemplate;

namespace Finance.PciDss.Bridge.Texcent.Server.Services
{
    public class TexcentGrpcService : IFinancePciDssBridgeGrpcService
    {
        private const string PaymentSystemId = "pciDssTexcentBankCards";
        private const string UsdCurrency = "USD";
        private readonly ILogger _logger;
        private readonly GrpcServiceClient<IMyCrmAuditLogGrpcService> _myCrmAuditLogGrpcService;
        private readonly ISettingsModelProvider _optionsMonitorSettingsModelProvider;
        private readonly ITexcentHttpClient _texcentHttpClient;

        public TexcentGrpcService(ITexcentHttpClient texcentHttpClient,
            GrpcServiceClient<IMyCrmAuditLogGrpcService> myCrmAuditLogGrpcService,
            ISettingsModelProvider optionsMonitorSettingsModelProvider,
            ILogger logger)
        {
            _texcentHttpClient = texcentHttpClient;
            _myCrmAuditLogGrpcService = myCrmAuditLogGrpcService;
            _optionsMonitorSettingsModelProvider = optionsMonitorSettingsModelProvider;
            _logger = logger;
        }

        private SettingsModel SettingsModel => _optionsMonitorSettingsModelProvider.Get();

        public async ValueTask<MakeBridgeDepositGrpcResponse> MakeDepositAsync(MakeBridgeDepositGrpcRequest request)
        {
            _logger.Information("TexcentGrpcService start process MakeBridgeDepositGrpcRequest {@request}", request);
            try
            {
                var loginResult = await _texcentHttpClient.LoginAsync();

                if (loginResult.IsFailed)
                {
                    await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                        $"{PaymentSystemId}. Fail texcent login. Message: {loginResult.FailedResult.Message}. Error: {JsonConvert.SerializeObject(loginResult.FailedResult.FieldError)}");
                    return MakeBridgeDepositGrpcResponse.Failed(DepositBridgeRequestGrpcStatus.ServerError,
                        loginResult.FailedResult.Message);
                }

                var token = loginResult.SuccessResult?.Data?.User?.Token;

                if (string.IsNullOrEmpty(token))
                {
                    await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                        $"{PaymentSystemId}. User token is null for traderId {request.PciDssInvoiceGrpcModel.TraderId}");
                    return MakeBridgeDepositGrpcResponse.Failed(DepositBridgeRequestGrpcStatus.ServerError,
                        " User token is null");
                }

                var createInvoiceRequest = request.PciDssInvoiceGrpcModel.ToCreatePaymentInvoiceRequest(SettingsModel);
                var createInvoiceResult =
                    await _texcentHttpClient.RegisterInvoiceAsync(createInvoiceRequest,
                        loginResult.SuccessResult.Data.User.Token);

                if (createInvoiceResult.IsFailed)
                {
                    await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                        $"{PaymentSystemId}. Fail texcent create invoice. Message: {createInvoiceResult.FailedResult.Message}. " +
                        $"Error: {JsonConvert.SerializeObject(createInvoiceResult.FailedResult.FieldError)}");
                    return MakeBridgeDepositGrpcResponse.Failed(DepositBridgeRequestGrpcStatus.ServerError,
                        createInvoiceResult.FailedResult.Message);
                }

                await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                    $"Created deposit invoice with id {request.PciDssInvoiceGrpcModel.OrderId}");

                return MakeBridgeDepositGrpcResponse.Create(createInvoiceResult.SuccessResult.Data.Payment.PaymentUrl,
                    createInvoiceResult.SuccessResult.Data.Payment.TransactionId,
                    DepositBridgeRequestGrpcStatus.Success);
            }
            catch (Exception e)
            {
                _logger.Error(e, "MakeDepositAsync failed for traderId {traderId}",
                    request.PciDssInvoiceGrpcModel.TraderId);
                await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                    $"{PaymentSystemId}. MakeDeposit failed");
                return MakeBridgeDepositGrpcResponse.Failed(DepositBridgeRequestGrpcStatus.ServerError, e.Message);
            }
        }

        public ValueTask<GetPaymentSystemGrpcResponse> GetPaymentSystemNameAsync()
        {
            return new ValueTask<GetPaymentSystemGrpcResponse>(GetPaymentSystemGrpcResponse.Create(PaymentSystemId));
        }

        public ValueTask<GetPaymentSystemCurrencyGrpcResponse> GetPsCurrencyAsync()
        {
            return new ValueTask<GetPaymentSystemCurrencyGrpcResponse>(
                GetPaymentSystemCurrencyGrpcResponse.Create(UsdCurrency));
        }

        public ValueTask<GetPaymentSystemAmountGrpcResponse> GetPsAmountAsync(GetPaymentSystemAmountGrpcRequest request)
        {
            if (request.Currency.Equals(UsdCurrency, StringComparison.OrdinalIgnoreCase))
                return new ValueTask<GetPaymentSystemAmountGrpcResponse>(
                    GetPaymentSystemAmountGrpcResponse.Create(request.Amount, UsdCurrency));

            return new ValueTask<GetPaymentSystemAmountGrpcResponse>((GetPaymentSystemAmountGrpcResponse) null);
        }

        public ValueTask<GetDepositStatusGrpcResponse> GetDepositStatusAsync(GetDepositStatusGrpcRequest request)
        {
            throw new NotImplementedException();
        }

        public ValueTask<DecodeBridgeInfoGrpcResponse> DecodeInfoAsync(DecodeBridgeInfoGrpcRequest request)
        {
            throw new NotImplementedException();
        }

        public ValueTask<MakeConfirmGrpcResponse> MakeDepositConfirmAsync(MakeConfirmGrpcRequest request)
        {
            throw new NotImplementedException();
        }

        private ValueTask SendMessageToAuditLogAsync(IPciDssInvoiceModel invoice, string message)
        {
            return _myCrmAuditLogGrpcService.Value.SaveAsync(new AuditLogEventGrpcModel
            {
                TraderId = invoice.TraderId,
                Action = "deposit",
                ActionId = invoice.OrderId,
                DateTime = DateTime.UtcNow,
                Message = message
            });
        }
    }
}