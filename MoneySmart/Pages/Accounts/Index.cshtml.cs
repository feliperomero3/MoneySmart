using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Account> Account { get; set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Accounts
                .OrderBy(a => a.Number)
                .ToListAsync();
        }
    }
}
