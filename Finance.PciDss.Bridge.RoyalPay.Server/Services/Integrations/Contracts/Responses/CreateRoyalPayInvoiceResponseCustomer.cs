using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Responses
{
    public class CreateRoyalPayInvoiceResponseCustomer
    {
        [JsonProperty("ip")] public string Ip { get; set; }
        [LogMasked(ShowFirst = 3, ShowLast = 3, PreserveLength = true)]
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("device_id")] public string DeviceId { get; set; }
        [JsonProperty("birth_date")] public string BirthDate { get; set; }
    }
}