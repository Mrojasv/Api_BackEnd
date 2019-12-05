using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly InvoiceContent _context;

        public InvoiceController(InvoiceContent context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoice/5
        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<ActionResult<Response>> GetInvoice(Guid id)
        {
            var InvoiveData = await _context.Invoices.FindAsync(id);
            if (InvoiveData == null)
            {
                return NotFound();
            }

            var ClientData = await _context.Clients.FindAsync(id);
            var PaymentData = await _context.Payments.Where(w => w.invoice_id == id).ToArrayAsync();

            var result = new Response();
            result.invoice_id = InvoiveData.invoice_id;
            result.client = ClientData;
            result.payments = PaymentData;

            return result;
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice data)
        {
            data.invoice_id = Guid.NewGuid();
            _context.Invoices.Add(data);

            data.client.invoice_id = data.invoice_id;
            _context.Clients.Add(data.client);

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInvoice", new { id = data.invoice_id }, data);
            return CreatedAtAction(nameof(GetInvoice), new { id = data.invoice_id }, data);
        }

        //// PUT: api/Invoice/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Invoice>> DeleteInvoice(Guid id)
        {
            var InvoiveData = await _context.Invoices.FindAsync(id);
            if (InvoiveData == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(InvoiveData);
            await _context.SaveChangesAsync();

            return InvoiveData;
        }
    }
}
