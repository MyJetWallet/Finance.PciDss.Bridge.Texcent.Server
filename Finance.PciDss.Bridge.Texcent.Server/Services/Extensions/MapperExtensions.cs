using System.Diagnostics;
using System.Globalization;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.Texcent.Server.Services.Integrations.Contracts.Requests;
using Flurl;

namespace Finance.PciDss.Bridge.Texcent.Server.Services.Extensions
{
    public static class MapperExtensions
    {
        public static CreatePaymentInvoiceRequest ToCreatePaymentInvoiceRequest(this IPciDssInvoiceModel model, SettingsModel settingsModel)
        {
            var activityId = Activity.Current?.Id;
            return new CreatePaymentInvoiceRequest
            {
                CardNo = model.CardNumber,
                OrderId = model.OrderId,
                Amount = model.Amount.ToString(CultureInfo.InvariantCulture),
                PayerEmail = model.Email,
                Currency = model.Currency,
                PayerName = model.FullName,
                RedirectUrl =
                    model.GetRedirectUrlForInvoice(settingsModel.RedirectMapping, settingsModel.DefaultRedirectUrl)
                        .SetQueryParam(nameof(activityId), activityId),
                NotifyUrl = settingsModel.TexcentNotifyUrl.SetQueryParam(nameof(activityId), activityId),
                Cvv2 = model.Cvv,
                ExpDate = model.ExpirationDate.ToString("MM-yyyy")
            };
        }
    }
}
