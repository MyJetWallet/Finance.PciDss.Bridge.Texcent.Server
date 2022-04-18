using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoicePayment
    {
        [JsonProperty("auth_code")] public string AuthCode { get; set; }
        [JsonProperty("bank_code")] public string BankCode { get; set; }
        [JsonProperty("rrn")] public string Rrn { get; set; }
        [JsonProperty("ref_id")] public string RefId { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("amount")] public double Amount { get; set; }
        [JsonProperty("currency")] public string Currency { get; set; }
        [JsonProperty("billing_descriptor")] public string BillingDescriptor { get; set; }
        [JsonProperty("gateway_id")] public long GatewayId { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
    }
}