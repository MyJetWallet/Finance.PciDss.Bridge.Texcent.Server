using System;
using System.Diagnostics;
using Finance.PciDss.Abstractions;
using Finance.PciDss.Bridge.RoyalPay.Server.Services.Integrations.Contracts.Requests;
using Finance.PciDss.PciDssBridgeGrpc;
using Flurl;

namespace Finance.PciDss.Bridge.RoyalPay.Server.Services.Extensions
{
    public static class MapperExtensions
    {
        public static CreateRoyalPayInvoice ToRoyalPayRestModel(this IPciDssInvoiceModel model, SettingsModel settingsModel)
        {
            var lastName = model.GetLastName(24);
            var firstName = model.GetName(24);

            var activityId = Activity.Current?.Id;

            return new CreateRoyalPayInvoice
            {
                Request = new CreateRoyalPayInvoiceRequest
                {
                    Amount = Convert.ToInt32(model.PsAmount * 100),
                    Currency = model.PsCurrency,
                    Description = "Platform deposit",
                    TrackingId = model.OrderId,
                    Language = "en",
                    NotifyUrl = settingsModel.RoyalPayNotifyUrl.SetQueryParam(nameof(activityId), activityId),
                    RedirectUrl = settingsModel.RoyalPayRedirectUrl.SetQueryParam(nameof(activityId), activityId),
                    Test = false,
                    BillingAddress = new CreateRoyalPayInvoiceRequestBillingAddress
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Country = model.Country,
                        City = model.City,
                        State = "none",
                        PostalCode = model.Zip,
                        Address = model.Address
                    },
                    CreditCard = new CreateRoyalPayInvoiceRequestCreditCard
                    {
                        Number = model.CardNumber,
                        Cvv = model.Cvv,
                        Holder = model.FullName.Length > 35 ? model.FullName[..35] : model.FullName,
                        ExpMount = model.ExpirationDate.ToString("MM"),
                        ExpYear = model.ExpirationDate.ToString("yyyy")
                    },
                    Customer = new CreateRoyalPayInvoiceRequestCustomer
                    {
                        Ip = model.Ip,
                        Email = model.Email
                    }
                }
            };
        }
    }
}
