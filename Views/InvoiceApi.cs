using Api_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Views
{
    public class InvoiceRequest
    {
        public virtual Product[] lines { get; set; }
        public virtual Client client { get; set; }
    }

    public class InvoiceApi
    {

        [Key]
        public Guid invoice_id { get; set; }

        public Product[] lines { get; set; }

        public virtual Client client { get; set; }

        public decimal tax_total { get; set; }
        public decimal discount_total { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }

        public virtual Payment[] payments { get; set; }
        public decimal balance { get; set; }
    }
}
