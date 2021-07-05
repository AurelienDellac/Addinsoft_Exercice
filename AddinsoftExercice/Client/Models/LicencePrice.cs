using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Models
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Une licence coûte ")
           .Append(Unit)
           .Append(" ")
           .Append(Currency)
           .Append(".");

            if (Quantity > 1)
            {
                builder.Append(" Le prix de ")
                    .Append(Quantity)
                    .Append(" licences est donc de : ")
                    .Append(Total)
                    .Append(" ")
                    .Append(Currency);
            }
            else
            {
                //do nothing
            }

            return builder.ToString();
        }
    }
}
