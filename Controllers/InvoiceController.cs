using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_BackEnd.Models;
using Api_BackEnd.Services;
using Api_BackEnd.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly InvoiceContent _context;
        private readonly InvoiceServices _service;

        public InvoiceController(InvoiceContent context)
        {
            _context = context;
            _service = new InvoiceServices(context);
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceApi>>> GetInvoices()
        {
            var _invoices = await _context.Invoices.ToListAsync();
            var _invoicesResponse = new List<InvoiceApi>();

            foreach (Invoice i in _invoices)
            {
                var _response = new InvoiceApi();
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

            return Ok(new { response = _invoicesResponse });
        }

        // GET: api/Invoice/5
        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<ActionResult<InvoiceApi>> GetInvoice(Guid id)
        {
            var _invoive = await _context.Invoices.FindAsync(id);
            if (_invoive == null)
            {
                return NotFound();
            }

            var _client = await _context.Clients.FindAsync(id);
            var _products = await _context.Products.Where(w => w.invoice_id == id).ToArrayAsync();
            var _payments = await _context.Payments.Where(w => w.invoice_id == id).ToArrayAsync();

            var result = new InvoiceApi();
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
        public async Task<ActionResult<Invoice>> PostInvoice([FromBody] InvoiceRequest data)
        {
            var result = await _service.CreateInvoice(data);
            return result;
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
