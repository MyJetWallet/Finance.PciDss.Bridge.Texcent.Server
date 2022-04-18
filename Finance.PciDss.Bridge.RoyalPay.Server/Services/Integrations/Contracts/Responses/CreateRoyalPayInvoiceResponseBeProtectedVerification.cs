using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseBeProtectedVerification
    {
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("white_black_list")]
        public CreateRoyalPayInvoiceResponseWhiteBlackList WhiteBlackList { get; set; }
    }
}