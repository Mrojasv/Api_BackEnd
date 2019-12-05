using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class Invoice
    {
        [Key]
        [Display(AutoGenerateField = true)]
        public Guid invoice_id { get; set; }
        public virtual Lines lines { get; set; }
        public virtual Client client { get; set; }
    }

    public class Lines
    {
        //public Guid idIProduct { get; set; }
        [Key]
        public string product { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal tax_rate { get; set; }
        public decimal discount_rate { get; set; }
        public string currency { get; set; }
    }

    public class Client
    {
        [Key]
        [Display(Name = "invoiceid", AutoGenerateField =true)]
        [Column("invoice")]
        public Guid invoice_id { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "id cannot be longer than 12 characters.")]
        public string id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "name cannot be longer than 150 characters.")]
        public string name { get; set; }
        
    }


    public class Response {

        [Key]
        public Guid invoice_id { get; set; }

        public Lines[] lines { get; set; }

        public Client client { get; set; }
        public PaymentInvoice[] payments { get; set; }
    }
}
