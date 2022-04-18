using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceBeProtectedVerification
    {
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("white_black_list")] public object WhiteBlackList { get; set; }
        [JsonProperty("rules")] public object Rules { get; set; }
    }
}