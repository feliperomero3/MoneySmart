using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;
using MoneySmart.Pages.Transactions;

namespace MoneySmart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;
    private readonly ApplicationDbContext _context;

    public TransactionsController(ILogger<TransactionsController> logger, ApplicationDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult> GetTransaction(long id)
    {
        var transaction = await _context.Transactions
            .AsNoTracking()
            .Include(t => t.Account)
            .OrderByDescending(t => t.DateTime)
            .Select(t => TransactionModel.MapFromTransaction(t))
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Transaction {Id} found.", id);

        return Ok(transaction);
    }
}
