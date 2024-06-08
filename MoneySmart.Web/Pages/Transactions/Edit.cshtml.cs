using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TransactionEditModel TransactionEditModel { get; set; }

        public SelectList Accounts { get; private set; }
        public SelectList TransactionTypes => new(TransactionType.Values);

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            TransactionEditModel = TransactionEditModel.MapFromTransaction(transaction);

            Accounts = new SelectList(await _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToListAsync(), "Id", "Name",
                TransactionEditModel.AccountId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = new SelectList(await _context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToListAsync(), "Id", "Name",
                    TransactionEditModel.AccountId);

                return Page();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == TransactionEditModel.Id);

            var account = await _context.Accounts.FindAsync(TransactionEditModel.AccountId);

            var modifiedTransaction = TransactionEditModel.MapToTransaction(account);

            transaction.EditTransaction(modifiedTransaction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(TransactionEditModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }
    }
}
