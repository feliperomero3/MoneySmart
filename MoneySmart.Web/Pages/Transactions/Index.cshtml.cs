using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Transactions
{
    [ResponseCache]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<TransactionModel> Transactions { get; private set; }

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .OrderByDescending(t => t.DateTime)
                .Select(t => TransactionModel.MapFromTransaction(t))
                .ToListAsync();
        }
    }
}
