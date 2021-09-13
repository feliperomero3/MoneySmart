using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneySmart.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public PageResult OnGet()
        {
            return Page();
        }
    }
}
