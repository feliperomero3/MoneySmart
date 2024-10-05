using System;
using System.Collections.Generic;
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
    [ResponseCache(Duration = 60)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITelemetryService _telemetryClient;

        public CreateModel(ApplicationDbContext context, ITelemetryService telemetryClient)
        {
            _context = context;
            _telemetryClient = telemetryClient;
        }

        public SelectList Accounts =>
            new(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name", TransactionCreateModel?.AccountId);

        public SelectList TransactionTypes => new(TransactionType.Values, TransactionType.Expense);

        [BindProperty]
        public TransactionInputModel TransactionCreateModel { get; set; }

        public class TransactionInputModel
        {
            [Required]
            [Display(Name = "Date")]
            public DateTime DateTime { get; init; }

            [Required]
            [DisplayName("Account")]
            public long AccountId { get; init; }

            [Required]
            [DisplayName("Description")]
            public string Description { get; init; }

            [Required]
            [DisplayName("Type")]
            public string TransactionType { get; init; }

            [Required]
            [DataType(DataType.Currency)]
            public decimal Amount { get; init; }

            [DataType(DataType.MultilineText)]
            public string Note { get; init; }

            public Transaction MapToTransaction(Account account)
            {
                return new Transaction(DateTime, account, Description,
                    (TransactionType)TransactionType, Amount, Note);
            }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _context.Accounts.FindAsync(TransactionCreateModel.AccountId);

            var transaction = TransactionCreateModel.MapToTransaction(account);

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            _telemetryClient.TrackEvent(TelemetryConstants.TransactionCreatedSuccessfully, new Dictionary<string, string>() { { "User", User.Identity.Name } });

            return RedirectToPage("./Index");
        }
    }
}
