using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneySmart.Pages
{
    [AllowAnonymous]
    public class PrivacyModel : PageModel
    {
        public PageResult OnGet()
        {
            return Page();
        }
    }
}