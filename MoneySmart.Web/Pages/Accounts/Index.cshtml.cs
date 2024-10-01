using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Accounts
{
    [ResponseCache(Duration = 60)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<AccountModel> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            Accounts = await _context.Accounts
                .AsNoTracking()
                .Include(t => t.Transactions)
                .OrderBy(a => a.Name)
                .Select(a => AccountModel.MapFromAccount(a))
                .ToListAsync();
        }
    }
}
