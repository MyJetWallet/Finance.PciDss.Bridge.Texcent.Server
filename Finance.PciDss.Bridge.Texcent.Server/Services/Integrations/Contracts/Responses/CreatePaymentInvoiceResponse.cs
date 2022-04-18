using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses
{
    public class CreatePaymentInvoiceResponse
    {
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("statusCode")] public int StatusCode { get; set; }
        [JsonProperty("data")] public CreatePaymentInvoiceResponseData Data { get; set; }
    }
}