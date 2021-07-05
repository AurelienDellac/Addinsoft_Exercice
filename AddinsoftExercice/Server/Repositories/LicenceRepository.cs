using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Server.Repositories
{
    public class LicenceRepository
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client = new HttpClient();

        public LicenceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private void ConfigureClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Addinsoft Alternance Exercice");
        }
        public async Task<LicencePrice> FetchLicencePrice(int quantity)
        {
            Uri uri = BuildUri(
                configuration["AddinsoftXlstatApiUrl"],
                new Dictionary<string, string>() 
                {
                    { "quantity", quantity.ToString() }
                });

            ConfigureClient();
            client.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration["Addinsoft:XlstatApiToken"]);

            return await JsonSerializer.DeserializeAsync<LicencePrice>(await GetResponse(uri).Result.Content.ReadAsStreamAsync());
        }

        public async Task<double> GetCurrencyEquivalence(string originCurrency, string targetCurrency)
        {
            string conversion = new StringBuilder(originCurrency).Append("_").Append(targetCurrency).ToString();
            Uri uri = BuildUri(
                configuration["CurrencyConvertApiUrl"],
                new Dictionary<string, string>()
                {
                    { "apiKey", configuration["Addinsoft:CurrencyConvertApiKey"]},
                    { "q",  conversion},
                    { "compact", "ultra" }
                });

            ConfigureClient();
            string stringValue = JObject.Parse(await GetResponse(uri).Result.Content.ReadAsStringAsync())[conversion].ToString();
            return double.Parse(stringValue);
        }

        private async Task<HttpResponseMessage> GetResponse(Uri uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private Uri BuildUri(String uri, Dictionary<String, String> queryParameters)
        {
            UriBuilder uriBuilder = new UriBuilder(uri);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (KeyValuePair<String, String> entry in queryParameters) {
                query[entry.Key] = entry.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }
    }
}
