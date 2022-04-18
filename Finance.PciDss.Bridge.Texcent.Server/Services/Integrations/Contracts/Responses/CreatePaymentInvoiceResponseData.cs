using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses
{
    public class CreatePaymentInvoiceResponseData
    {
        [JsonProperty("payment")] public CreatePaymentInvoiceResponseDataPayment Payment { get; set; }
    }
}