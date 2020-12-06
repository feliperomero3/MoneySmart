using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public readonly SelectList TransactionTypes = new SelectList(new[] { "Income", "Expense" });

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList Accounts { get; private set; }

        public IActionResult OnGet()
        {
            Accounts = new SelectList(_context.Accounts.AsNoTracking().ToList(), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public TransactionCreateModel TransactionCreateModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = new SelectList(_context.Accounts.AsNoTracking().ToList(), "Id", "Name",
                    TransactionCreateModel.AccountId);

                return Page();
            }

            var account = _context.Accounts.Find(TransactionCreateModel.AccountId);

            var transaction = TransactionCreateModel.MapToTransaction(account);

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

    public class TransactionCreateModel
    {
        [Required]
        [Display(Name = "Date")]
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
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public Transaction MapToTransaction(Account account)
        {
            return new Transaction(DateTime, account, Description,
                (TransactionType)Enum.Parse(typeof(TransactionType), TransactionTypeName), Amount);
        }
    }
}
