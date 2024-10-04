using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySmart.Data;
using MoneySmart.Domain;
using MoneySmart.Telemetry;

namespace MoneySmart.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly TelemetryClient _telemetryClient;

        public CreateModel(ApplicationDbContext context, TelemetryClient telemetryClient)
        {
            _context = context;
            _telemetryClient = telemetryClient;
        }

        [BindProperty]
        public AccountInputModel Account { get; set; }

        public class AccountInputModel
        {
            public long Number { get; set; }
            public string Name { get; set; }

            public Account MapToAccount()
            {
                return new Account(Number, Name);
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

            var account = Account.MapToAccount();

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            _telemetryClient.TrackEvent(TelemetryConstants.AccountCreatedSuccessfully);

            return RedirectToPage("./Index");
        }
    }
}