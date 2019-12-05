using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class PaymentInvoice
    {
        [Key]
        public Guid payment_id { get; set; }

        public Guid invoice_id { get; set; }
        public decimal amount { get; set; }
    }
}
