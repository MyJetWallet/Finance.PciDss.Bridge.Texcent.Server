using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseAvsCvcVerification
    {
        [JsonProperty("avs_verification")]
        public CreateRoyalPayInvoiceResponseAvsVerification AvsVerification { get; set; }

        [JsonProperty("cvc_verification")]
        public CreateRoyalPayInvoiceResponseCvcVerification CvcVerification { get; set; }
    }
}