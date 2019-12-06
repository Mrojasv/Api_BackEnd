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

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "GetPayment")]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var _payment = await _context.Payments.FindAsync(id);

            if (_payment == null)
            {
                return NotFound();
            }

            return _payment;
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment data)
        {
            var _invoive = await _context.Invoices.FindAsync(data.invoice_id);
            if (_invoive == null)
            {
                return NotFound();
            }

            if (_invoive.balance < data.amount)
            {
                return BadRequest();
            }

            _invoive.balance = (_invoive.balance - data.amount);

            data.payment_id = Guid.NewGuid();
            _context.Payments.Add(data);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = data.payment_id }, data);
        }

    }
}
