using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class Product
    {
        [Display(Name = "invoiceid", AutoGenerateField = true)]
        [Column("invoice")]
        public Guid invoice_id { get; set; }

        [Key]
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "product cannot be longer than 30 characters.")]
        public string product { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal tax_rate { get; set; }
        public decimal discount_rate { get; set; }
        public string currency { get; set; }
    }
}
