using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using System;
using System.Collections.Generic;
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
            }

            try
            {
                licencePrice = await this.licenceRepository.ProcessLicencePrice(quantity);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }

            //handle bad currency
            return licencePrice;
        }


    }
}
