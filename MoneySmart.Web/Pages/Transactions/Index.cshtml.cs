using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<TransactionModel> Transactions { get; private set; }

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .OrderByDescending(t => t.DateTime)
                .ThenByDescending(t => t.Id)
                .Select(t => TransactionModel.MapFromTransaction(t))
                .ToListAsync();
        }
    }

    public class TransactionModel
    {
        [DisplayName("Number")]
        public long Id { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime DateTime { get; set; }

        [DisplayName("Account")]
        public string AccountName { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Type")]
        public string TransactionType { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public static TransactionModel MapFromTransaction(Transaction transaction)
        {
            return new TransactionModel
            {
                Id = transaction.Id,
                DateTime = transaction.DateTime,
                AccountName = transaction.Account.Name,
                Description = transaction.Description,
                TransactionType = transaction.TransactionType.ToString(),
                Amount = transaction.Amount
            };
        }
    }
}
