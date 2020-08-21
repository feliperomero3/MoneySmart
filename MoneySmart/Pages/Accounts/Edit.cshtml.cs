using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

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

        private Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);

            if (Account == null)
            {
                return NotFound();
            }

            AccountModel = AccountEditModel.FromAccount(Account);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Account = await _context.Accounts.FindAsync(AccountModel.Id);

            if (Account == null)
            {
                return new UnprocessableEntityResult();
            }

            Account.EditAccount(AccountModel.ToAccount());

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.Id))
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

    public class AccountEditModel
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }

        public static AccountEditModel FromAccount(Account account)
        {
            return new AccountEditModel
            {
                Id = account.Id,
                Number = account.Number,
                Name = account.Name
            };
        }

        public Account ToAccount()
        {
            return new Account(Name, Number);
        }
    }
}
