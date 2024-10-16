using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Telemetry;

namespace MoneySmart.Pages.Transactions
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITelemetryService _telemetry;

        public DeleteModel(ApplicationDbContext context, ITelemetryService telemetry)
        {
            _context = context;
            _telemetry = telemetry;
        }

        public TransactionModel Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            Transaction = TransactionModel.MapFromTransaction(transaction);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);

                await _context.SaveChangesAsync();

                _telemetry.TrackEvent(TelemetryConstants.TransactionDeletedSuccessfully, User.Identity!.Name);
            }

            return RedirectToPage("./Index");
        }
    }
}
