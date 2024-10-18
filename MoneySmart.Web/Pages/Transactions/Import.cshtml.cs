using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class ImportModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ImportModel> _logger;

    public ImportModel(ApplicationDbContext context, ILogger<ImportModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    private class TransactionImportModel
    {
        public DateTime DateTime { get; set; }

        public long AccountId { get; set; }

        public string AccountName { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }
    }

    private Transaction MapToTransaction(TransactionImportModel transaction)
    {
        var account = _context.Accounts.Find(transaction.AccountId);

        var dateTime = transaction.DateTime;
        var description = transaction.Description;
        var transactionType = (TransactionType)transaction.TransactionType;
        var amount = transaction.Amount;
        var note = transaction.Note;

        return new Transaction(dateTime, account, description, transactionType, amount, note);
    }

    public ActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UploadedFile == null || UploadedFile.Length == 0)
        {
            ModelState.AddModelError(nameof(UploadedFile), "Please select a valid CSV file.");

            return Page();
        }

        await ImportTransactionsAsync();

        return RedirectToPage("./Index");
    }

    private async Task ImportTransactionsAsync()
    {
        var filePath = Path.GetTempFileName();

        await using (var stream = System.IO.File.Create(filePath))
        {
            _logger.LogDebug("Uploaded file '{UploadedFileName}' is {Lenght} bytes.", UploadedFile.FileName, UploadedFile.Length);

            await UploadedFile.CopyToAsync(stream);
        }

        _logger.LogDebug("Uploaded file '{UploadedFileName}' created at '{FilePath}'", UploadedFile.FileName, filePath);

        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        {
            var firstLine = await reader.ReadLineAsync();

            _logger.LogDebug("Started processing the CSV file.");
            _logger.LogDebug("Reading the file line by line...");

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                _logger.LogDebug("ReadLineAsync(): " + line);

                if (line is null)
                {
                    break;
                }

                var values = line.Split(',');
                var importModel = new TransactionImportModel
                {
                    DateTime = DateTime.ParseExact(values[0], "dd/MM/yyyy HH:mm:ss", provider: null),
                    AccountId = long.Parse(values[1]),
                    AccountName = values[2],
                    Description = values[3],
                    TransactionType = values[4],
                    Amount = decimal.Parse(values[5]),
                    Note = values[6]
                };

                var newTransaction = MapToTransaction(importModel);

                _logger.LogDebug("Adding the transaction to the database context: {Transaction}", newTransaction);

                _context.Transactions.Add(newTransaction);
            }

            _logger.LogDebug("Reached the end of the CSV file.");
            _logger.LogDebug("Saving changes to the database.");

            var newTransactionsCount = await _context.SaveChangesAsync();

            _logger.LogDebug("Added {NewTransactionsCount} transactions", newTransactionsCount);
        }

        _logger.LogDebug("Finished processing the CSV file.");
    }
}