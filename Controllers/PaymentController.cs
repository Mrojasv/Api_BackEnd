using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly InvoiceContent _context;

        public PaymentController(InvoiceContent context)
        {
            _context = context;
        }

        //// GET: api/Payment
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "GetPayment")]
        public async Task<ActionResult<PaymentInvoice>> GetPayment(Guid id)
        {
            var PaymentData = await _context.Payments.FindAsync(id);

            if (PaymentData == null)
            {
                return NotFound();
            }

            return PaymentData;
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<PaymentInvoice>> PostPayment(PaymentInvoice data)
        {
            data.payment_id = Guid.NewGuid();
            _context.Payments.Add(data);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPayment", new { id = data.payment_id }, data);
            return CreatedAtAction(nameof(GetPayment), new { id = data.payment_id }, data);
        }

        //// PUT: api/Payment/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
