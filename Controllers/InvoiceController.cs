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
        public async Task<ActionResult<IEnumerable<InvoiceResponse>>> GetInvoices()
        {
            var _invoices = await _context.Invoices.ToListAsync();
            var _invoicesResponse = new List<InvoiceResponse>();

            foreach (Invoice i in _invoices)
            {
                var _response = new InvoiceResponse();
                var _client = await _context.Clients.FindAsync(i.invoice_id);
                var _products = await _context.Products.Where(w => w.invoice_id == i.invoice_id).ToArrayAsync();
                var _payments = await _context.Payments.Where(w => w.invoice_id == i.invoice_id).ToArrayAsync();

                _response.invoice_id = i.invoice_id;
                _response.lines = _products;
                _response.client = _client;
                _response.payments = _payments;
                _response.tax_total = i.tax_total;
                _response.discount_total = i.discount_total;
                _response.subtotal = i.subtotal;
                _response.total = i.total;
                _response.balance = i.balance;

                _invoicesResponse.Add(_response);
            }

            return _invoicesResponse;
        }

        // GET: api/Invoice/5
        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<ActionResult<InvoiceResponse>> GetInvoice(Guid id)
        {
            var _invoive = await _context.Invoices.FindAsync(id);
            if (_invoive == null)
            {
                return NotFound();
            }

            var _client = await _context.Clients.FindAsync(id);
            var _products = await _context.Products.Where(w => w.invoice_id == id).ToArrayAsync();
            var _payments = await _context.Payments.Where(w => w.invoice_id == id).ToArrayAsync();

            var result = new InvoiceResponse();
            result.invoice_id = _invoive.invoice_id;
            result.lines = _products;
            result.client = _client;
            result.payments = _payments;
            result.tax_total = _invoive.tax_total;
            result.discount_total = _invoive.discount_total;
            result.subtotal = _invoive.subtotal;
            result.total = _invoive.total;
            result.balance = _invoive.balance;

            return result;
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceRequest data)
        {
            var _invoice_id = Guid.NewGuid();
            var _invoice = new Invoice { invoice_id = _invoice_id };

            var _products = data.lines.ToArray();
            foreach (Product p in _products)
            {
                p.invoice_id = _invoice_id;
                var _subtotal = (p.price * p.quantity);
                var _discount_total = (_subtotal * p.discount_rate / 100);
                var _tax_total = ((_subtotal- _discount_total) * p.tax_rate / 100);

                _invoice.subtotal = _invoice.subtotal + _subtotal;
                _invoice.tax_total = _invoice.tax_total + _tax_total;
                _invoice.discount_total = _invoice.discount_total + _discount_total;
                _invoice.total = _invoice.balance + ((_subtotal - _discount_total) + _tax_total);
                _invoice.balance = _invoice.total;
            }
            data.client.invoice_id = _invoice_id;

            _context.Invoices.Add(_invoice);
            _context.Products.AddRange(_products);
            _context.Clients.Add(data.client);
            await _context.SaveChangesAsync();

            return _invoice;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Invoice>> DeleteInvoice(Guid id)
        {
            var _invoive = await _context.Invoices.FindAsync(id);
            if (_invoive == null)
            {
                return NotFound();
            }
            var _client = await _context.Clients.FindAsync(id);
            var _products = await _context.Products.Where(w => w.invoice_id == id).ToArrayAsync();
            var _payments = await _context.Payments.Where(w => w.invoice_id == id).ToArrayAsync();

            _context.Invoices.Remove(_invoive);
            _context.Clients.Remove(_client);
            _context.Products.RemoveRange(_products);
            _context.Payments.RemoveRange(_payments);
            await _context.SaveChangesAsync();

            return _invoive;
        }
    }
}
