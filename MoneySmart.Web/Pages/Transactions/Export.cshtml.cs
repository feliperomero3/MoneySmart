using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class ExportModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ExportModel> _logger;

    public ExportModel(ApplicationDbContext context, ILogger<ExportModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    private class TransactionExportModel
    {
        public DateTime DateTime { get; set; }

        public long AccountId { get; set; }

        public string AccountName { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }

        public static TransactionExportModel MapToTransactionOutputModel(Transaction transaction)
        {
            return new TransactionExportModel
            {
                DateTime = transaction.DateTime,
                AccountId = transaction.Account.Id,
                AccountName = transaction.Account.Name,
                Description = transaction.Description,
                TransactionType = transaction.TransactionType.ToString(),
                Amount = transaction.Amount,
                Note = transaction.Note
            };
        }
    }

    public void OnGet()
    {
    }

    /// <summary>
    /// Export transactions to a file.
    /// </summary>
    /// <returns>A file with all the transactions.</returns>
    public async Task<FileResult> OnGetFileAsync()
    {
        var transactions = await _context.Transactions
            .AsNoTracking()
            .Include(t => t.Account)
            .OrderByDescending(t => t.DateTime)
            .Select(t => TransactionExportModel.MapToTransactionOutputModel(t))
            .ToListAsync();

        _logger.LogDebug("Found {Count} transactions available to export.", transactions.Count);

        var csv = new StringBuilder();
        csv.AppendLine("DateTime,AccountId,AccountName,Description,TransactionType,Amount,Note");

        foreach (var transaction in transactions)
        {
            csv.AppendLine(
                $"{transaction.DateTime},{transaction.AccountId},{transaction.AccountName},{transaction.Description},{transaction.TransactionType},{transaction.Amount},{transaction.Note}");
        }

        var bytes = Encoding.UTF8.GetBytes(csv.ToString());

        return File(bytes, "text/csv;charset=utf-8", "transactions.csv");
    }
}