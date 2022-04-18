using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseAvsVerification
    {
        [JsonProperty("result_code")] public string ResultCode { get; set; }
    }
}