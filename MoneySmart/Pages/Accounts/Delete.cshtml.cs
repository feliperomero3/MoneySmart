using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccountModel Account { get; set; }

        public async Task<IActionResult> OnGetAsync(string number)
        {
            if (number == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Number == number);

            if (account == null)
            {
                return NotFound();
            }

            Account = AccountModel.FromAccount(account);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);

            if (account != null)
            {
                _context.Accounts.Remove(account);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }

    public class AccountModel
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }

        public static AccountModel FromAccount(Account account)
        {
            return new AccountModel
            {
                Id = account.Id,
                Number = account.Number,
                Name = account.Name
            };
        }
    }
}
