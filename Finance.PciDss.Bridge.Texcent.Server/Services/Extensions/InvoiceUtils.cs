using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.PciDss.Abstractions;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Extensions
{
    public static class InvoiceUtils
    {
        public static string GetRedirectUrlForInvoice(this IPciDssInvoiceModel invoice,
            string mappingString, string defaultRedirectUrl)
        {
            var mapping =
                mappingString
                    .Split("|")
                    .Select(item => item.Split("@"))
                    .Select(item => RedirectUrlSettings.Create(item[0], item[1], item[2]));

            foreach (var redirectUrlSettings in mapping)
            {
                if (invoice.BrandName.Equals(redirectUrlSettings.Brand, StringComparison.OrdinalIgnoreCase) && invoice.AccountId.Contains(redirectUrlSettings.AccountPrefix))
                {
                    return redirectUrlSettings.Link;
                }
            }

            return defaultRedirectUrl;
        }

        private class RedirectUrlSettings
        {
            public string Brand { get; private set; }
            public string AccountPrefix { get; private set; }
            public string Link { get; private set; }

            public static RedirectUrlSettings Create(string brand, string accountPrefix, string link)
            {
                return new RedirectUrlSettings {Brand = brand,AccountPrefix = accountPrefix, Link = link };
            }
        }
    }
}
