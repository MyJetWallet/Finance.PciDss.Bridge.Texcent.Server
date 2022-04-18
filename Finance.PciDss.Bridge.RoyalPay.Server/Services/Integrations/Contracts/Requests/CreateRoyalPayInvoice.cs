using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests
{
    public class CreateRoyalPayInvoice
    {
        [JsonProperty("request")] public CreateRoyalPayInvoiceRequest Request { get; set; }
    }
}