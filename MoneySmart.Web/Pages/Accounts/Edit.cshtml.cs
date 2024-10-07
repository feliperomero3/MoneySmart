using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccountEditModel AccountModel { get; set; }

        public async Task<IActionResult> OnGetAsync(long? number)
        {
            if (number == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Number == number);

            if (account == null)
            {
                return NotFound();
            }

            AccountModel = AccountEditModel.MapFromAccount(account);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Number == AccountModel.Number);

            if (account == null)
            {
                return NotFound();
            }

            account.EditAccount(AccountModel.MapToAccount());

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool AccountExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
