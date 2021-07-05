using Client.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Client.Repositories
{
    public class LicenceRepository
    {
        private readonly HttpClient client = new HttpClient();

        private void ConfigureClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Addinsoft Alternance Exercice");
        }

        public async Task<String> GetLicencePricePreview(int quantity, string currency)
        {
            string preview;
            HttpResponseMessage response = await FetchLicencePrice(quantity, currency);

            if(response.IsSuccessStatusCode)
            {
                preview = BuildLicencePreview(
                    await JsonSerializer.DeserializeAsync<LicencePrice>(await response.Content.ReadAsStreamAsync()));
            } else
            {
                preview = JObject.Parse(await response.Content.ReadAsStringAsync())["message"].ToString();
            }

            return preview;
        }

        private async Task<HttpResponseMessage> FetchLicencePrice(int quantity, string currency)
        {
            Uri uri = BuildUri(
                ConfigurationManager.AppSettings.Get("LicencePriceApiUri"),
                new Dictionary<string, string>()
                {
                    { "quantity", quantity.ToString() },
                    { "currency", currency }
                });

            ConfigureClient();
            return await client.GetAsync(uri);
        }

        private Uri BuildUri(String uri, Dictionary<String, String> queryParameters)
        {
            UriBuilder uriBuilder = new UriBuilder(uri);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (KeyValuePair<String, String> entry in queryParameters)
            {
                if(!string.IsNullOrEmpty(entry.Value))
                {
                    query[entry.Key] = entry.Value;
                } else
                {
                    //do nothing
                }
                
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }
        private string BuildLicencePreview(LicencePrice licencePrice)
        {
            StringBuilder builder = new StringBuilder("Une licence coûte ")
            .Append(licencePrice.Unit)
            .Append(" ")
            .Append(licencePrice.Currency)
            .Append(".");

            if (licencePrice.Quantity > 1)
            {
                builder.Append(" Le prix de ")
                    .Append(licencePrice.Quantity)
                    .Append(" licences est donc de : ")
                    .Append(licencePrice.Total)
                    .Append(" ")
                    .Append(licencePrice.Currency);
            } else
            {
                //do nothing
            }

            return builder.ToString();
        }
    }
}
