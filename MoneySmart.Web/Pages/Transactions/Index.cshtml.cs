using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Transactions
{
    [ResponseCache(Duration = 60)]
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

        public async Task<ActionResult> OnGetTransactionDetails(long id)
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .Where(t => t.Id == id)
                .Select(t => TransactionModel.MapFromTransaction(t))
                .FirstOrDefaultAsync();

            return transaction == null
                ? Partial("_TransactionDetails")
                : Partial("_TransactionDetails", transaction);
        }
    }
}
