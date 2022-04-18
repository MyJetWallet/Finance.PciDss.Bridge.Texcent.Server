using SimpleTrading.SettingsReader;

namespace Finance.PciDss.Bridge.RoyalPay.Server
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("PciDssBridgeRoyalPay.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.AuditLogGrpcServiceUrl")]
        public string AuditLogGrpcServiceUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.ConvertServiceGrpcUrl")]
        public string ConvertServiceGrpcUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.RoyalPayApiUrl")]
        public string RoyalPayApiUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.RoyalPayRedirectUrl")]
        public string RoyalPayRedirectUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.RoyalPayNotifyUrl")]
        public string RoyalPayNotifyUrl { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.RoyalPayUsername")]
        public string RoyalPayUsername { get; set; }

        [YamlProperty("PciDssBridgeRoyalPay.RoyalPayPassword")]
        public string RoyalPayPassword { get; set; }
    }
}