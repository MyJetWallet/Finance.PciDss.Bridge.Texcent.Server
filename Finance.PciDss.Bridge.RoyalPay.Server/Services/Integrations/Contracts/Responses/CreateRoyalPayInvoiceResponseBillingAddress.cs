using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseBillingAddress
    {
        [JsonProperty("first_name")] public string FirstName { get; set; }
        [JsonProperty("last_name")] public string LastName { get; set; }
        [JsonProperty("address")] public string Address { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("zip")] public string Zip { get; set; }
        [JsonProperty("state")] public string State { get; set; }
        [JsonProperty("phone")] public string Phone { get; set; }
    }
}