using System.Threading.Tasks;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations
{
    public interface IRoyalPayHttpClient
    {
        Task<Response<CreateRoyalPayInvoiceResponse, string>> RegisterInvoiceAsync(
            CreateRoyalPayInvoice request, string brandName);
    }
}