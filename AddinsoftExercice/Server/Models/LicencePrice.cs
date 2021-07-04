using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Server.Models
{
    public class LicencePrice
    {
        [JsonPropertyName("unit")]
        public double Unit { get; set; }
        [JsonPropertyName("total")]
        public double Total { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("currency")]
        public String Currency { get; set; }
    }
}
