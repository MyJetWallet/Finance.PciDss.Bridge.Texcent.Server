using Destructurama.Attributed;
using Newtonsoft.Json;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Requests
{
    public class LoginRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [NotLogged]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}