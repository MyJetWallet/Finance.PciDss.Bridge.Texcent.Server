using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Requests
{
    public class CreatePaymentInvoiceRequest
    {
        [JsonProperty("orderId")] public string OrderId { get; set; }
        [JsonProperty("amount")] public string Amount { get; set; }
        [JsonProperty("payerEmail")] public string PayerEmail { get; set; }
        [JsonProperty("currency")] public string Currency { get; set; }
        [JsonProperty("payerName")] public string PayerName { get; set; }
        [LogMasked(ShowFirst = 6, ShowLast = 4, PreserveLength = true)]
        [JsonProperty("cardNo")] public string CardNo { get; set; }
        [NotLogged]
        [JsonProperty("expDate")] public string ExpDate { get; set; }
        [NotLogged]
        [JsonProperty("cvv2")] public string Cvv2 { get; set; }
        [JsonProperty("redirectUrl")] public string RedirectUrl { get; set; }
        [JsonProperty("notifyUrl")] public string NotifyUrl { get; set; }
    }
}