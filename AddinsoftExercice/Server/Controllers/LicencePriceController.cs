using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencePriceController : ControllerBase
    {
        // GET: api/<LicencePriceController>
        [HttpGet]
        public ActionResult<LicencePrice> Get(int quantity, string currency)
        {
            if (quantity <= 0)
            {
                return BadRequest("Quantity must be more than 0");
            }

            //handle bad currency
            LicencePrice quote = new LicencePrice();
            quote.Quantity = quantity;
            quote.Currency = currency;
            return quote;
        }
    }
}
