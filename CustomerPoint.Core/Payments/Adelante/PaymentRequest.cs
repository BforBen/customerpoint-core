using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CustomerPoint.Payments.Adelante
{
    /// <summary>
    /// Class for making a payment request via Adelante
    /// </summary>
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            ReturnMethod = "redirect";
            Items = new List<PaymentRequestItem>();
        }
        /// <summary>
        /// The channel to use for processing the payment
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// The transaction reference
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// The billing address UPRN - this will be use to look up the address
        /// </summary>
        public long? Uprn { get; set; }
        /// <summary>
        /// The URL to return the user to following payment (successful or otherwise)
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// The method to use to return the user. Options are either "POST" or "REDIRECT"
        /// </summary>
        public string ReturnMethod { get; set; }
        /// <summary>
        /// The username of the person taking the payment
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// The items to include in the payment
        /// </summary>
        public IEnumerable<PaymentRequestItem> Items { get; set; }

        public string GetRedirectUrl()
        {
            var TotalToPay = Items.Sum(i => i.Amount);

            if (TotalToPay > 0)
            {
                GuildfordBoroughCouncil.Address.Models.Address a = null;

                if (Uprn.HasValue)
                {
                    a = GuildfordBoroughCouncil.Address.Lookup.AddressDetailsByUprn(Uprn.Value).Result.SingleOrDefault();
                }

                var i = 0;
                var ItemQuery = "";
                foreach (var item in Items)
                {
                    var q = item.ToQueryString(i);
                    if (!string.IsNullOrWhiteSpace(q))
                    {
                        ItemQuery += "&" + q;
                        i++;
                    }
                }

                var PaymentTemplate = Properties.Settings.Default.AdelantePaymentsUri + "wsconn_pay.asp?channel={0}&sessionid={1}&chouse={2}&cadd1={3}&ctown={4}&cpostcode={5}&returnmethod={6}&returnurl={7}{8}";

                var Url = string.Format(PaymentTemplate,
                        Channel,
                        WebUtility.UrlEncode(Reference),
                        WebUtility.UrlEncode(a != null ? a.Property : ""),
                        WebUtility.UrlEncode(a != null ? a.Street : ""),
                        WebUtility.UrlEncode(a != null ? a.Town : ""),
                        WebUtility.UrlEncode(a != null ? a.PostCode : ""),
                        ReturnMethod,
                        WebUtility.UrlEncode(ReturnUrl),
                        ItemQuery);

                return Url;
            }
            else if (TotalToPay == 0)
            {
                throw new Exception("You don't have anything to pay.");
            }
            else
            {
                throw new Exception("You need a refund.");
            }
        }
    }

    public class PaymentRequestItem
    {
        public PaymentRequestItem()
        {
            References = new string[4];
        }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string LedgerCode { get; set; }
        public string Fund { get; set; }
        public IEnumerable<string> References { get; set; }

        public string ToQueryString(int? Iterator = null)
        {
            if (Amount <= 0 || string.IsNullOrWhiteSpace(Fund) || (string.IsNullOrWhiteSpace(LedgerCode) && References.Count() == 0))
            {
                return null;
            }

            var Template = "amount{0}={1}&fundcode{0}={2}&custref1{0}={3}&custref2{0}={4}&custref3{0}={5}&custref4{0}={6}&description{0}={7}";
            var PostFix = (Iterator.HasValue && Iterator.Value > 0) ? "_a" + Iterator.Value.ToString() : "";

            var lcExists = !string.IsNullOrWhiteSpace(LedgerCode);

            return string.Format(Template,
                PostFix,
                (int)(Amount * 100),
                Fund,
                WebUtility.UrlEncode(lcExists ? LedgerCode : References.FirstOrDefault()),
                WebUtility.UrlEncode(lcExists ? References.FirstOrDefault() ?? "" : References.Skip(1).FirstOrDefault() ?? ""),
                WebUtility.UrlEncode(lcExists ? References.Skip(1).FirstOrDefault() ?? "" : References.Skip(2).FirstOrDefault() ?? ""),
                WebUtility.UrlEncode(lcExists ? References.Skip(2).FirstOrDefault() ?? "" : References.Skip(3).FirstOrDefault() ?? ""),
                WebUtility.UrlEncode(Description)
                );
        }
    }
}
