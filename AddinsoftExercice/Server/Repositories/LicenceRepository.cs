using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<LicencePrice> ProcessLicencePrice(int quantity)
        {
            UriBuilder uriBuilder = new UriBuilder(configuration["AddinsoftXlstatApiUrl"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["quantity"] = quantity.ToString();
            uriBuilder.Query = query.ToString();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Addinsoft Alternance Exercice");
            client.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration["Addinsoft:XlstatApiToken"]);

            HttpResponseMessage response = await client.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<LicencePrice>(await response.Content.ReadAsStreamAsync());
        }
    }
}
