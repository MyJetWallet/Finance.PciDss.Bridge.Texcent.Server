using System.Threading.Tasks;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Requests;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses;
using Flurl;
using Flurl.Http;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations
{
    public class TexcentHttpClient : ITexcentHttpClient
    {
        private readonly ISettingsModelProvider _settingsModelProvider;

        public TexcentHttpClient(ISettingsModelProvider settingsModelProvider)
        {
            _settingsModelProvider = settingsModelProvider;
        }

        private SettingsModel SettingsModel => _settingsModelProvider.Get();

        public async Task<Response<LoginResponse,LoginFail>> LoginAsync()
        {
            var result = await SettingsModel
                .TexcentPciDssBaseUrl
                .AppendPathSegments("api", "login")
                .AllowHttpStatus("400")
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(new LoginRequest
                {
                    Email = SettingsModel.TexcentEmail,
                    Password = SettingsModel.TexcentPassword
                });

            return await result.DeserializeTo<LoginResponse, LoginFail>();
        }

        public async Task<Response<CreatePaymentInvoiceResponse, CreatePaymentInvoiceFailResponseDataPayment>> RegisterInvoiceAsync(CreatePaymentInvoiceRequest request,
            string bearerToken)
        {
            var result = await SettingsModel
                .TexcentPciDssBaseUrl
                .AppendPathSegments("api", "payments")
                .WithHeader("Content-Type", "application/json")
                .AllowHttpStatus("400")
                .WithOAuthBearerToken(bearerToken)
                .PostJsonAsync(request);

            return await result.DeserializeTo<CreatePaymentInvoiceResponse, CreatePaymentInvoiceFailResponseDataPayment>();
        }
    }
}