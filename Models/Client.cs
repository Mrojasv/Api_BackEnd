using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BackEnd.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "invoiceid", AutoGenerateField = true)]
        [Column("invoice")]
        public Guid invoice_id { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "id cannot be longer than 12 characters.")]
        public string id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "name cannot be longer than 150 characters.")]
        public string name { get; set; }
    }
}
