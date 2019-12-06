using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class Product
    {
        [Display(Name = "invoiceid", AutoGenerateField = true)]
        [Column("invoice")]
        [JsonIgnore]
        public Guid invoice_id { get; set; }

        [Key]
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "product cannot be longer than 30 characters.")]
        public string product { get; set; }
        [Required]
        [Range(1, 999999)]
        public int quantity { get; set; }
        [Range(0, 99999999.99)]
        public decimal price { get; set; }
        [Range(0, 100)]
        public decimal tax_rate { get; set; }
        [Range(0, 100)]
        public decimal discount_rate { get; set; }
        [Remote(action: "VerifyCurrency", controller: "Validator", ErrorMessage= "Only local currency.")]
        public string currency { get; set; }
    }
}
