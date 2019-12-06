using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        [AcceptVerbs("POST")]
        public IActionResult VerifyCurrency(string currency)
        {
            if (currency.ToUpper() != "CRC")
            {
                return BadRequest();
            }
            return Ok(true);
        }
    }
}