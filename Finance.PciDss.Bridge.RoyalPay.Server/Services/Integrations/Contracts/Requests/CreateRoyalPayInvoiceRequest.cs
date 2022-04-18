using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests
{
    public class CreateRoyalPayInvoiceRequest
    {
        [JsonProperty("amount")] public int Amount { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("tracking_id")] public string TrackingId { get; set; }

        [JsonProperty("language")] public string Language { get; set; }
        [JsonProperty("notification_url")] public string NotifyUrl { get; set; }
        [JsonProperty("return_url")] public string RedirectUrl { get; set; }
        [JsonProperty("test")] public bool Test { get; set; }
        [JsonProperty("billing_address")] public CreateRoyalPayInvoiceRequestBillingAddress BillingAddress { get; set; }
        [JsonProperty("credit_card")] public CreateRoyalPayInvoiceRequestCreditCard CreditCard { get; set; }
        [JsonProperty("customer")] public CreateRoyalPayInvoiceRequestCustomer Customer { get; set; }
    }
}