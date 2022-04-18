using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseTransaction
    {
        [JsonProperty("uid")] public string Uid { get; set; }
        [JsonProperty("Id")] public string Id { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("amount")] public string Amount { get; set; }
        [JsonProperty("currency")] public string Currency { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("payment_method_type")] public string PaymentMethodType { get; set; }
        [JsonProperty("tracking_id")] public string TrackingId { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("test")] public string Test { get; set; }
        [JsonProperty("created_at")] public string CreatedAt { get; set; }
        [JsonProperty("updated_at")] public string UpdatedAt { get; set; }
        [JsonProperty("paid_at")] public string PaidAt { get; set; }
        [JsonProperty("expired_at")] public string ExpiredAt { get; set; }
        [JsonProperty("closed_at")] public string ClosedAt { get; set; }
        [JsonProperty("settled_at")] public string SettledAt { get; set; }
        [JsonProperty("language")] public string Language { get; set; }
        [JsonProperty("redirect_url")] public string RedirectUrl { get; set; }
        [JsonProperty("payment")] public CreateRoyalPayInvoicePayment Payment { get; set; }
        [JsonProperty("credit_card")] public CreateRoyalPayInvoiceCreditCard CreditCard { get; set; }
        [JsonProperty("customer")] public CreateRoyalPayInvoiceResponseCustomer Customer { get; set; }

        [JsonProperty("billing_address")]
        public CreateRoyalPayInvoiceResponseBillingAddress BillingAddress { get; set; }

        [JsonProperty("be_protected_verification")]
        public CreateRoyalPayInvoiceBeProtectedVerification BeProtectedVerification { get; set; }

        public bool IsFailed()
        {
            return Status.Equals("failed", System.StringComparison.OrdinalIgnoreCase);
        }
    }
}