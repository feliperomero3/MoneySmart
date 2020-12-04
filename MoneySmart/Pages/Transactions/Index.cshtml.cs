using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TransactionModel> Transactions { get; set; }

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions
                .Include(t => t.Account)
                .Select(t => new TransactionModel
                {
                    Id = t.Id,
                    DateTime = t.DateTime,
                    AccountName = t.Account.Name,
                    Description = t.Description,
                    TransactionTypeName = t.TransactionType.ToString(),
                    Amount = t.Amount
                })
                .ToListAsync();
        }
    }

    public class TransactionModel
    {
        public long Id { get; set; }

        [Display(Name = "Date")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Type")]
        public string TransactionTypeName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
