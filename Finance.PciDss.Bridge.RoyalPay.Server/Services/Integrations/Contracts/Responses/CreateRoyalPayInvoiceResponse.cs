using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponse
    {
        [JsonProperty("transaction")] public CreateRoyalPayInvoiceResponseTransaction Transaction { get; set; }
    }
}
