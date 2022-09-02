using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList SourceAccounts { get; private set; }
        public SelectList DestinationAccounts { get; private set; }

        [BindProperty]
        public TransferInputModel TransferModel { get; set; }

        public class TransferInputModel
        {
            [Required]
            [DisplayName("Date")]
            public DateTime DateTime { get; set; } = DateTime.Now;

            [DisplayName("Source Account")]
            public long SourceAccountId { get; set; }

            [DisplayName("Destination Account")]
            public long DestinationAccountId { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            public decimal Amount { get; set; }

            public string Notes { get; set; }
        }

        public void OnGet()
        {
            SourceAccounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name");
            DestinationAccounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name");
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                SourceAccounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name", TransferModel.SourceAccountId);
                DestinationAccounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name", TransferModel.DestinationAccountId);

                return Page();
            }

            var sourceAccount = _context.Accounts.Find(TransferModel.SourceAccountId);
            var destinationAccount = _context.Accounts.Find(TransferModel.DestinationAccountId);

            var transfer = new Transfer
            {
                Notes = TransferModel.Notes
            };

            var transaction1 = new Transaction(TransferModel.DateTime, sourceAccount, TransferModel.Description, TransactionType.Expense, TransferModel.Amount, transfer);
            var transaction2 = new Transaction(TransferModel.DateTime, destinationAccount, TransferModel.Description, TransactionType.Income, TransferModel.Amount, transfer);

            _context.Transactions.AddRange(transaction1, transaction2);
            _context.SaveChanges();

            return RedirectToPage("../Transactions/Index");
        }
    }
}
