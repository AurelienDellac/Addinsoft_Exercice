using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencePriceController : ControllerBase
    {
        private readonly LicenceRepository licenceRepository;
        public LicencePriceController(LicenceRepository licenceRepository)
        {
            this.licenceRepository = licenceRepository;
        }

        // GET: api/<LicencePriceController>
        [HttpGet]
        public async Task<ActionResult<LicencePrice>> Get(int quantity, string currency)
        {
            LicencePrice licencePrice;
                        
            if (quantity <= 0)
            {
                return BadRequest(new {message = "quantity must be more than zero"});
            } else
            {
                //do nothing
            }

            var currencyCultures = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Where(c => new RegionInfo(c.LCID).ISOCurrencySymbol == currency.ToUpper())
                .ToArray();

            if (currencyCultures.Length <= 0)
            {
                return BadRequest(new { message = "currency not valid" });
            } else
            {
                //do nothing
            }

            try
            {
                licencePrice = await this.licenceRepository.FetchLicencePrice(quantity);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }

            double conversionCoef = await licenceRepository.GetCurrencyEquivalence(licencePrice.Currency.ToUpper(), currency.ToUpper());
            licencePrice.Unit = Math.Round(licencePrice.Unit * conversionCoef, 2);
            licencePrice.Total = Math.Round(licencePrice.Unit * licencePrice.Quantity, 2);
            licencePrice.Currency = currency;
            return licencePrice;
        }


    }
}
