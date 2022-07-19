﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Accounts
{
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
            var accounts = await _context.Accounts
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync();

            Accounts = accounts.ConvertAll(AccountModel.FromAccount);
        }
    }
}
