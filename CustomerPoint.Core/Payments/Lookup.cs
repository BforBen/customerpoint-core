using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPoint.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CustomerPoint.Payments
{
    public static class Lookup
    {
        public static async Task<IEnumerable<Transaction>> Payments(string id = null, IEnumerable<string> reference = null, string fund = null, string ledgerCode = null, DateTime? from = null, DateTime? to = null, IEnumerable<string> methods = null, IEnumerable<string> xmethods = null)
        {
            using (var hc = new HttpClient())
            {
                hc.BaseAddress = Properties.Settings.Default.PaymentsServiceUri;
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                List<string> QueryString = new List<string>();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    QueryString.Add("id=" + id);
                }
                else
                {
                    if (reference != null && reference.Count() > 0)
                    {
                        QueryString.AddRange(reference.Select(r => "reference[]=" + r));
                    }

                    if (!string.IsNullOrWhiteSpace(fund))
                    {
                        QueryString.Add("fund=" + fund);
                    }

                    if (!string.IsNullOrWhiteSpace(ledgerCode))
                    {
                        QueryString.Add("ledgerCode=" + ledgerCode);
                    }

                    if (from.HasValue)
                    {
                        QueryString.Add("from=" + from.Value.ToString("yyyy-MM-dd"));
                    }

                    if (to.HasValue)
                    {
                        QueryString.Add("to=" + to.Value.ToString("yyyy-MM-dd"));

                        if (!from.HasValue)
                        {
                            QueryString.Add("from=" + System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString("yyyy-MM-dd"));
                        }
                    }

                    if (methods != null && methods.Count() > 0)
                    {
                        QueryString.AddRange(methods.Select(r => "methods[]=" + r));
                    }

                    if (xmethods != null && xmethods.Count() > 0)
                    {
                        QueryString.AddRange(xmethods.Select(r => "xmethods[]=" + r));
                    }
                }

                var response = await hc.GetAsync("v1/transactions?" + String.Join("&", QueryString));

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Transaction>>();
            }
        }
    }
}
