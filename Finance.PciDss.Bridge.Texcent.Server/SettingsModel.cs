using SimpleTrading.SettingsReader;

namespace Finance.PciDss.Bridge.Texcent.Server
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("PciDssBridgeTexcent.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("PciDssBridgeTexcent.TexcentPciDssBaseUrl")]
        public string TexcentPciDssBaseUrl { get; set; }

        [YamlProperty("PciDssBridgeTexcent.TexcentEmail")]
        public string TexcentEmail { get; set; }

        [YamlProperty("PciDssBridgeTexcent.TexcentPassword")]
        public string TexcentPassword { get; set; }

        [YamlProperty("PciDssBridgeTexcent.TexcentNotifyUrl")]
        public string TexcentNotifyUrl { get; set; }

        [YamlProperty("PciDssBridgeTexcent.DefaultRedirectUrl")]
        public string DefaultRedirectUrl { get; set; }

        //{brand}@{prefix}@{redirectUrl}|{brand}@{prefix}@{redirectUrl} 
        [YamlProperty("PciDssBridgeTexcent.DepositRedirectMapping")]
        public string RedirectMapping { get; set; }

        [YamlProperty("PciDssBridgeTexcent.AuditLogGrpcServiceUrl")]
        public string AuditLogGrpcServiceUrl { get; set; }
    }
}
