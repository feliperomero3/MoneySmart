using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList Accounts =>
            new(_context.Accounts.AsNoTracking().OrderBy(a => a.Name).ToList(), "Id", "Name", TransactionEditModel?.AccountId);

        public SelectList TransactionTypes => new(TransactionType.Values);

        [BindProperty]
        public TransactionInputModel TransactionEditModel { get; set; }

        public class TransactionInputModel
        {
            [Required]
            [DisplayName("Number")]
            public long Id { get; init; }

            [Required]
            [DisplayName("Date")]
            public DateTime DateTime { get; init; }

            [Required]
            [DisplayName("Account")]
            public long AccountId { get; init; }

            [Required]
            [DisplayName("Description")]
            public string Description { get; init; }

            [Required]
            [DisplayName("Type")]
            public string TransactionType { get; init; }

            [Required]
            [DataType(DataType.Currency)]
            public decimal Amount { get; init; }

            [DataType(DataType.MultilineText)]
            public string Note { get; init; }

            public static TransactionInputModel MapFromTransaction(Transaction transaction)
            {
                return new TransactionInputModel
                {
                    Id = transaction.Id,
                    DateTime = transaction.DateTime,
                    AccountId = transaction.Account.Id,
                    Description = transaction.Description,
                    TransactionType = transaction.TransactionType.ToString(),
                    Amount = transaction.Amount,
                    Note = transaction.Note
                };
            }

            public Transaction MapToTransaction(Account account)
            {
                return new Transaction(DateTime, account, Description,
                    (TransactionType)TransactionType, Amount, Note);
            }
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            TransactionEditModel = TransactionInputModel.MapFromTransaction(transaction);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == TransactionEditModel.Id);

            if (transaction == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(TransactionEditModel.AccountId);

            var modifiedTransaction = TransactionEditModel.MapToTransaction(account);

            transaction.EditTransaction(modifiedTransaction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(TransactionEditModel.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }
    }
}
