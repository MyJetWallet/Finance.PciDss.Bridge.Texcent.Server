using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations;
using Microsoft.Extensions.DependencyInjection;
using MyCrm.AuditLog.Grpc;
using Serilog;
using SimpleTrading.GrpcTemplate;
using SimpleTrading.MyLogger;
using SimpleTrading.SettingsReader;

namespace Finance.PciDss.Bridge.Texcent.Server
{
    public static class ServicesBinder
    {
        public static string AppName { get; private set; } = "Finance.PciDss.BridgeTexcent.Server";

        public static void BindTexcentHttpCLient(this IServiceCollection services)
        {
            services.AddSingleton<ITexcentHttpClient, TexcentHttpClient>();
        }

        public static void BindLogger(this IServiceCollection services, SettingsModel settings)
        {
            var logger = new MyLogger(AppName, settings.SeqServiceUrl);
            Log.Logger = logger;
            services.AddSingleton<ILogger>(logger);
        }

        public static void BindSettings(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<ISettingsModelProvider, SettingsModelProvider>();
        }

        public static void BindGrpcServices(this IServiceCollection services, SettingsModel settings)
        {
            var client = new GrpcServiceClient<IMyCrmAuditLogGrpcService>(
                () => SettingsReader
                    .ReadSettings<SettingsModel>()
                    .AuditLogGrpcServiceUrl);
            services.AddSingleton(client);
        }
    }
}
