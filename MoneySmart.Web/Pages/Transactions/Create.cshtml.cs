using System.Collections.Generic;
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
        public SelectList TransactionTypes => new(TransactionType.Values);

        public async Task<IActionResult> OnGet()
        {
            Accounts = new SelectList(await _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToListAsync(), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public TransactionCreateModel TransactionCreateModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = new SelectList(await _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToListAsync(), "Id", "Name",
                    TransactionCreateModel.AccountId);

                return Page();
            }

            var account = await _context.Accounts.FindAsync(TransactionCreateModel.AccountId);

            var transaction = TransactionCreateModel.MapToTransaction(account);

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            _telemetryClient.TrackEvent("TransactionCreatedSuccessfully", new Dictionary<string, string>() { { "User", User.Identity.Name } });

            return RedirectToPage("./Index");
        }
    }
}
