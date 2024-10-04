using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;
using MoneySmart.Telemetry;

namespace MoneySmart.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITelemetryService _telemetry;

        public IndexModel(ApplicationDbContext context, ITelemetryService telemetry)
        {
            _context = context;
            _telemetry = telemetry;
        }

        public SelectList SourceAccounts { get; private set; }
        public SelectList DestinationAccounts { get; private set; }

        [BindProperty]
        public TransferInputModel TransferModel { get; set; }

        public class TransferInputModel
        {
            [Required]
            [DisplayName("Date")]
            public DateTime DateTime { get; init; } = DateTime.Now;

            [DisplayName("Source Account")]
            public long SourceAccountId { get; init; }

            [DisplayName("Destination Account")]
            public long DestinationAccountId { get; init; }

            [Required]
            public string Description { get; init; }

            [Required]
            public decimal Amount { get; init; }

            [DataType(DataType.MultilineText)]
            public string Notes { get; init; }
        }

        public void OnGet()
        {
            var accounts = _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList();

            SourceAccounts = new SelectList(accounts, "Id", "Name");
            DestinationAccounts = new SelectList(accounts, "Id", "Name");
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var accounts = _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList();

                SourceAccounts = new SelectList(accounts, "Id", "Name", TransferModel.SourceAccountId);
                DestinationAccounts = new SelectList(accounts, "Id", "Name", TransferModel.DestinationAccountId);

                return Page();
            }

            var sourceAccount = _context.Accounts.Find(TransferModel.SourceAccountId);
            var destinationAccount = _context.Accounts.Find(TransferModel.DestinationAccountId);

            var transfer = new Transfer
            {
                Notes = TransferModel.Notes
            };

            var transaction1 = new Transaction(TransferModel.DateTime, sourceAccount, TransferModel.Description, TransactionType.Expense, TransferModel.Amount, null, transfer);
            var transaction2 = new Transaction(TransferModel.DateTime, destinationAccount, TransferModel.Description, TransactionType.Income, TransferModel.Amount, null, transfer);

            _context.Transactions.AddRange(transaction1, transaction2);
            _context.SaveChanges();

            _telemetry.TrackEvent(TelemetryConstants.TransferCreatedSuccessfully, new Dictionary<string, string>() { { "User", User.Identity.Name } });

            return RedirectToPage("../Transactions/Index");
        }
    }
}
