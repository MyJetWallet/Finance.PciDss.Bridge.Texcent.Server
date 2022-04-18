using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseCvcVerification
    {
        [JsonProperty("result_code")] public string ResultCode { get; set; }
    }
}