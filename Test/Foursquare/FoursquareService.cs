using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.Foursquare
{
    public class FoursquareService
    {

        public FoursquareService()
        {
        }

        public async static Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var venues = new List<Venue>();

            using (var client = new HttpClient())
            {
                var url = GetUrl(latitude, longitude);
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
            }

            return venues;
        }

        public static string GetUrl(double latitude, double longitude)
        {
            return string.Format(Constants.Url, latitude, longitude, Constants.ClientId, Constants.ClientSecret, DateTime.Today.ToString("yyyyMMdd"));
        }
    }
}
