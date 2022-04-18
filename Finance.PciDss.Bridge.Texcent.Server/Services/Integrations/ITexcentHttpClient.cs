using System.Threading.Tasks;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Requests;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations
{
    public interface ITexcentHttpClient
    {
        Task<Response<LoginResponse, LoginFail>> LoginAsync();

        Task<Response<CreatePaymentInvoiceResponse, CreatePaymentInvoiceFailResponseDataPayment>> RegisterInvoiceAsync(
            CreatePaymentInvoiceRequest request,
            string bearerToken);
    }
}