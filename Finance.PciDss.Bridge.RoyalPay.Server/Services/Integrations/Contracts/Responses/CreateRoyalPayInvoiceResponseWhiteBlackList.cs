using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseWhiteBlackList
    {
        [LogMasked(ShowFirst = 3, ShowLast = 3, PreserveLength = true)]
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("ip")] public string Ip { get; set; }
        [LogMasked(ShowFirst = 6, ShowLast = 4, PreserveLength = true)]
        [JsonProperty("card_number")] public string CardNumber { get; set; }
    }
}