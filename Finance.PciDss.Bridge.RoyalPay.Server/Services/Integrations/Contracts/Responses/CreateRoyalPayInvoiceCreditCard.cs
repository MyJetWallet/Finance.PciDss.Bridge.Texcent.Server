using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceCreditCard
    {
        [LogMasked(ShowFirst = 1, ShowLast = 1, PreserveLength = true)]
        [JsonProperty("holder")] public string Holder { get; set; }
        [LogMasked(ShowFirst = 1, PreserveLength = true)]
        [JsonProperty("stamp")] public string Stamp { get; set; }
        [JsonProperty("brand")] public string Brand { get; set; }
        [JsonProperty("last_4")] public string Last4 { get; set; }
        [JsonProperty("first_1")] public string First1 { get; set; }
        [JsonProperty("bin")] public string Bin { get; set; }
        [JsonProperty("issuer_country")] public string IssuerCountry { get; set; }
        [LogMasked(ShowFirst = 1,ShowLast = 1, PreserveLength = true)]
        [JsonProperty("issuer_name")] public string IssuerName { get; set; }
        [JsonProperty("product")] public string Product { get; set; }
        [NotLogged]
        [JsonProperty("exp_month")] public string ExpMonth { get; set; }
        [NotLogged]
        [JsonProperty("exp_year")] public string ExpYear { get; set; }
        [JsonProperty("token_provider")] public string TokenProvider { get; set; }
        [LogMasked(ShowFirst = 1, PreserveLength = true)]
        [JsonProperty("token")] public string Token { get; set; }
    }
}