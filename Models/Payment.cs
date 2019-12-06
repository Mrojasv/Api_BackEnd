using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class Payment
    {
        [Key]
        public Guid payment_id { get; set; }
        [Required]
        public Guid invoice_id { get; set; }
        [Required]
        [Range(1, 99999999.99)]
        public decimal amount { get; set; }
    }
}
