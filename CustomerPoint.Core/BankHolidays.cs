using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace CustomerPoint
{
    public static partial class Data
    {
        public static async Task<List<DateTime>> BankHolidays()
        {
            // https://www.gov.uk/bank-holidays
            // http://loop.guildford.gov.uk/Lists/Bank%20holidays%20and%20office%20closures/AllItems.aspx

            var MemCache = MemoryCache.Default;

            var Dates = (List<DateTime>)MemCache.Get("BankHolidaysAndClosures");

            if (Dates == null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = Properties.Settings.Default.LookupServiceUri;

                    var response = client.GetAsync("BankHolidaysAndClosures").Result;

                    using (HttpContent content = response.Content)
                    {
                        Dates = await content.ReadAsAsync<List<DateTime>>();
                        MemCache.Add("BankHolidaysAndClosures", Dates, new CacheItemPolicy { });
                    }
                }
            }

            return Dates;
        }

    }
}
