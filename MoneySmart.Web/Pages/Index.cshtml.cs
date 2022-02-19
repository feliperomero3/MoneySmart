using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneySmart.Pages
{
    public class IndexModel : PageModel
    {
        public PageResult OnGet()
        {
            return Page();
        }
    }
}
