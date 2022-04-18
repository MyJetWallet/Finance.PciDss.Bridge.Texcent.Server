using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Integrations
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<Response<TSuccessResponse, TFailedResponse>> DeserializeTo<TSuccessResponse,
            TFailedResponse>(this HttpResponseMessage httpResponseMessage)
            where TSuccessResponse : class
            where TFailedResponse : class
        {
            string resultData = await httpResponseMessage.Content.ReadAsStringAsync();
            Log.Logger.Information("Texcent return response : {resultData}", resultData);
            try
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<TSuccessResponse>(resultData);
                    return Response<TSuccessResponse, TFailedResponse>.CreateSuccess(response);
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<TFailedResponse>(resultData);
                    return Response<TSuccessResponse, TFailedResponse>.CreateFailed(response);
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "DeserializeTo failed. Response : {resultData}", resultData);
                throw;
            }
        }
    }
}