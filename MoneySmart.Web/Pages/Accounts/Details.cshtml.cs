﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;

namespace MoneySmart.Pages.Accounts;

[ResponseCache(VaryByQueryKeys = new[] { "number" }, Duration = 60)]
public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public AccountModel Account { get; set; }

    public async Task<IActionResult> OnGetAsync(long? number)
    {
        if (number == null)
        {
            return NotFound();
        }

        var account = await _context.Accounts
            .AsNoTracking()
            .Include(t => t.Transactions)
            .OrderBy(a => a.Name)
            .FirstOrDefaultAsync(m => m.Number == number);

        if (account == null)
        {
            return NotFound();
        }

        Account = AccountModel.MapFromAccount(account);

        return Page();
    }
}