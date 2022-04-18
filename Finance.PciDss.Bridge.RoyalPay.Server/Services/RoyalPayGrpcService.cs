using System;
using System.Threading.Tasks;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Extensions;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations;
using Finance.PciDss.PciDssBridgeGrpc;
using Finance.PciDss.PciDssBridgeGrpc.Contracts;
using Finance.PciDss.PciDssBridgeGrpc.Contracts.Enums;
using MyCrm.AuditLog.Grpc;
using MyCrm.AuditLog.Grpc.Models;
using Serilog;
using SimpleTrading.Common.Helpers;
using SimpleTrading.ConvertService.Grpc;
using SimpleTrading.ConvertService.Grpc.Contracts;
using SimpleTrading.GrpcTemplate;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services
{
    public class RoyalPayGrpcService : IFinancePciDssBridgeGrpcService
    {
        private const string PaymentSystemId = "pciDssRoyalPayBankCards";
        private const string UsdCurrency = "USD";
        private const string EurCurrency = "EUR";
        private readonly GrpcServiceClient<IMyCrmAuditLogGrpcService> _myCrmAuditLogGrpcService;
        private readonly GrpcServiceClient<IConvertService> _convertServiceClient;
        private readonly ISettingsModelProvider _settingsModelProvider;
        private readonly ILogger _logger;
        private readonly IRoyalPayHttpClient _royalPayHttpClient;

        public RoyalPayGrpcService(IRoyalPayHttpClient royalPayHttpClient,
            GrpcServiceClient<IMyCrmAuditLogGrpcService> myCrmAuditLogGrpcService,
            GrpcServiceClient<IConvertService> convertServiceClient,
            ISettingsModelProvider settingsModelProvider,
            ILogger logger)
        {
            _royalPayHttpClient = royalPayHttpClient;
            _myCrmAuditLogGrpcService = myCrmAuditLogGrpcService;
            _convertServiceClient = convertServiceClient;
            _settingsModelProvider = settingsModelProvider;
            _logger = logger;
        }

        private SettingsModel SettingsModel => _settingsModelProvider.Get();

        public async ValueTask<MakeBridgeDepositGrpcResponse> MakeDepositAsync(MakeBridgeDepositGrpcRequest request)
        {
            _logger.Information("RoyalPayGrpcService start process MakeBridgeDepositGrpcRequest {@request}", request);
            try
            {
                request.PciDssInvoiceGrpcModel.Country = CountryManager.Iso3ToIso2(request.PciDssInvoiceGrpcModel.Country);

                var response =
                    await _royalPayHttpClient.RegisterInvoiceAsync(
                        request.PciDssInvoiceGrpcModel.ToRoyalPayRestModel(SettingsModel),
                        request.PciDssInvoiceGrpcModel.BrandName);

                if (response.IsFailed || response.SuccessResult?.Transaction?.IsFailed() == true)
                {
                    _logger.Information("Fail Royal Pay create invoice. {@response}", response);
                    await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                        $"Fail Royal Pay create invoice. Error {response.FailedResult ?? response.SuccessResult?.Transaction?.Message}");
                    return MakeBridgeDepositGrpcResponse.Failed(DepositBridgeRequestGrpcStatus.ServerError,
                        response.FailedResult ?? response.SuccessResult?.Transaction?.Message);
                }

                await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel, $"Created deposit invoice with id {request.PciDssInvoiceGrpcModel.OrderId}");
                return MakeBridgeDepositGrpcResponse.Create(response.SuccessResult?.Transaction?.RedirectUrl,
                    response.SuccessResult?.Transaction?.Id, DepositBridgeRequestGrpcStatus.Success);
            }
            catch (Exception e)
            {
                _logger.Error(e, "MakeDepositAsync failed for traderId {traderId}",
                    request.PciDssInvoiceGrpcModel.TraderId);
                await SendMessageToAuditLogAsync(request.PciDssInvoiceGrpcModel,
                    $"MakeDepositAsync failed for traderId {request.PciDssInvoiceGrpcModel.TraderId}");
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
                GetPaymentSystemCurrencyGrpcResponse.Create(EurCurrency));
        }

        public async ValueTask<GetPaymentSystemAmountGrpcResponse> GetPsAmountAsync(GetPaymentSystemAmountGrpcRequest request)
        {
            if (request.Currency.Equals(UsdCurrency, StringComparison.OrdinalIgnoreCase))
            {

                var convertResponse = await _convertServiceClient.Value.Convert(new CovertRequest
                {
                    InstrumentId = EurCurrency + UsdCurrency,
                    ConvertType = ConvertTypes.QuoteToBase,
                    Amount = request.Amount
                });

                return GetPaymentSystemAmountGrpcResponse.Create(convertResponse.ConvertedAmount, EurCurrency);
            }
                
            return default;
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