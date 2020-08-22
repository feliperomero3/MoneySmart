using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountInputModel Account { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = Account.ToAccount();

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

    public class AccountInputModel
    {
        public string Number { get; set; }
        public string Name { get; set; }

        public Account ToAccount()
        {
            return new Account(Number, Name);
        }
    }
}