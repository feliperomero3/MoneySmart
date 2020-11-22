using System;
using System.ComponentModel.DataAnnotations;

namespace MoneySmart.Models
{
    public class TransactionInputModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Account")]
        public long AccountId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string TransactionTypeName { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
