using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class LicencePrice
    {
        public double Unit { get; set; }
        public double Total { get; set; }
        public int Quantity { get; set; }
        public String Currency { get; set; }
    }
}
