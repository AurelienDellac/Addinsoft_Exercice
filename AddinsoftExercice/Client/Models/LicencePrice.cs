using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Models
{
    class LicencePrice
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
