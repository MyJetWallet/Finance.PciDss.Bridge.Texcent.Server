using System.Threading.Tasks;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Serilog;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations
{
    public class RoyalPayHttpClient : IRoyalPayHttpClient
    {
        private readonly ISettingsModelProvider _settingsModelProvider;

        public RoyalPayHttpClient(ISettingsModelProvider settingsModelProvider)
        {
            _settingsModelProvider = settingsModelProvider;
        }

        private SettingsModel SettingsModel => _settingsModelProvider.Get();

        public async Task<Response<CreateRoyalPayInvoiceResponse, string>> RegisterInvoiceAsync(
            CreateRoyalPayInvoice request, string brandName)
        {
            Log.Logger.Information("RoyalPay send request : {@requests}, link {link} royalpay user {userName}", request, SettingsModel.RoyalPayApiUrl, SettingsModel.RoyalPayUsername);
            var result = await SettingsModel
                .RoyalPayApiUrl
                .AppendPathSegments("transactions", "payments")
                .WithHeader("Content-Type", "application/json")
                .AllowHttpStatus("400,422")
                .WithBasicAuth(SettingsModel.RoyalPayUsername,
                    SettingsModel.RoyalPayPassword)
                .PostStringAsync(JsonConvert.SerializeObject(request));

            return await result.DeserializeTo<CreateRoyalPayInvoiceResponse, string>();
        }
    }
}