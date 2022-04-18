using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses
{
    public class CreatePaymentInvoiceResponseDataPayment
    {
        [JsonProperty("order_id")] public string OrderId { get; set; }
        [JsonProperty("p_order_id")] public string POrderId { get; set; }
        [JsonProperty("created_timestamp")] public string CreatedTimestamp { get; set; }
        [JsonProperty("transaction_id")] public string TransactionId { get; set; }
        [JsonProperty("payment_url")] public string PaymentUrl { get; set; }
        [JsonProperty("response_code")] public int ResponseCode { get; set; }

        [JsonProperty("response_msg")] public string ResponseMessage { get; set; }
    }
}