namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Responses
{
    public class LoginResponse
    {
        public LoginResponseData Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}