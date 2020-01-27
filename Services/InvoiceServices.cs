using Api_BackEnd.Models;
using Api_BackEnd.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Services
{
    public class InvoiceServices
    {
        private readonly InvoiceContent _context;

        public InvoiceServices(InvoiceContent context)
        {
            _context = context;
        }

        public async Task<Invoice> CreateInvoice(InvoiceRequest data)
        {
            var _invoice = new Invoice();

            var _products = data.lines.ToArray();
            foreach (Product p in _products)
            {
                if (p.currency.ToUpper() != "CRC")
                {
                    return null;//new BadRequestObjectResult("Only local currency");
                }

                p.invoice_id = _invoice.invoice_id;
                var _subtotal = (p.price * p.quantity);
                var _discount_total = (_subtotal * p.discount_rate / 100);
                var _tax_total = ((_subtotal - _discount_total) * p.tax_rate / 100);

                _invoice.subtotal = _invoice.subtotal + _subtotal;
                _invoice.tax_total = _invoice.tax_total + _tax_total;
                _invoice.discount_total = _invoice.discount_total + _discount_total;
                _invoice.total = _invoice.balance + ((_subtotal - _discount_total) + _tax_total);
                _invoice.balance = _invoice.total;
            }
            data.client.invoice_id = _invoice.invoice_id;

            _context.Invoices.Add(_invoice);
            _context.Products.AddRange(_products);
            _context.Clients.Add(data.client);
            await _context.SaveChangesAsync();

            return _invoice;
        }
    }
}
