using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;
using MoneySmart.Telemetry;

namespace MoneySmart.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITelemetryService _telemetryClient;

        public CreateModel(ApplicationDbContext context, ITelemetryService telemetryClient)
        {
            _context = context;
            _telemetryClient = telemetryClient;
        }

        public SelectList Accounts { get; private set; }
        public SelectList TransactionTypes => new SelectList(new[] { "Income", "Expense" }, "Expense");

        public IActionResult OnGet()
        {
            Accounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public TransactionCreateModel TransactionCreateModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = new SelectList(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name",
                    TransactionCreateModel.AccountId);

                return Page();
            }

            var account = await _context.Accounts.FindAsync(TransactionCreateModel.AccountId);

            var transaction = TransactionCreateModel.MapToTransaction(account);

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            _telemetryClient.TrackEvent("TransactionCreatedSuccessfully");

            return RedirectToPage("./Index");
        }
    }

    public class TransactionCreateModel
    {
        [Required]
        [Display(Name = "Date")]
        public DateTime DateTime { get; set; }

        [Required]
        [DisplayName("Account")]
        public long AccountId { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Type")]
        public string TransactionType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public Transaction MapToTransaction(Account account)
        {
            return new Transaction(DateTime, account, Description,
                (TransactionType)TransactionType, Amount);
        }
    }
}
