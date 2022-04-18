using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations;
using Microsoft.Extensions.DependencyInjection;
using MyCrm.AuditLog.Grpc;
using Serilog;
using SimpleTrading.ConvertService.Grpc;
using SimpleTrading.GrpcTemplate;
using SimpleTrading.MyLogger;
using SimpleTrading.SettingsReader;

namespace Finance.PciDss.Bridge.RoyalPay.Server
{
    public static class ServicesBinder
    {
        public static string AppName { get; private set; } = "Finance.PciDss.BridgeRoyalPay.Server";

        public static void BindRoyalPayHttpCLient(this IServiceCollection services)
        {
            services.AddSingleton<IRoyalPayHttpClient, RoyalPayHttpClient>();
        }

        public static void BindLogger(this IServiceCollection services, SettingsModel settings)
        {
            var logger = new MyLogger(AppName, settings.SeqServiceUrl);
            services.AddSingleton<ILogger>(logger);
            Log.Logger = logger;
        }

        public static void BindSettings(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<ISettingsModelProvider, SettingsModelProvider>();
        }

        public static void BindGrpcServices(this IServiceCollection services, SettingsModel settings)
        {
            var clientAuditLogGrpcService = new GrpcServiceClient<IMyCrmAuditLogGrpcService>(
                () => SettingsReader
                    .ReadSettings<SettingsModel>()
                    .AuditLogGrpcServiceUrl);

            services.AddSingleton(clientAuditLogGrpcService);


            var clientConvertService = new GrpcServiceClient<IConvertService>(
                () => SettingsReader
                    .ReadSettings<SettingsModel>()
                    .ConvertServiceGrpcUrl);

            services.AddSingleton(clientConvertService);
        }
    }
}
