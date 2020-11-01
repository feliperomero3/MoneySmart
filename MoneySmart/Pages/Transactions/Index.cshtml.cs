using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get; set; }

        public async Task OnGetAsync()
        {
            Transaction = await _context.Transactions.ToListAsync();
        }
    }
}
