using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests
{
    public class CreateRoyalPayInvoiceRequestCreditCard
    {
        [LogMasked(ShowFirst = 6, ShowLast = 4, PreserveLength = true)]
        [JsonProperty("number")] public string Number { get; set; }
        [NotLogged]
        [JsonProperty("verification_value")] public string Cvv { get; set; }
        [LogMasked(ShowFirst = 1, ShowLast = 1, PreserveLength = true)]
        [JsonProperty("holder")] public string Holder { get; set; }
        [NotLogged]
        [JsonProperty("exp_month")] public string ExpMount { get; set; }
        [NotLogged]
        [JsonProperty("exp_year")] public string ExpYear { get; set; }
    }
}