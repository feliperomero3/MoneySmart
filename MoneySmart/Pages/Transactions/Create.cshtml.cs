﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public SelectList Accounts;
        public SelectList TransactionTypes = new SelectList(new[] { "Income", "Expense" });

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Accounts = new SelectList(_context.Accounts.AsNoTracking().ToList(), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public TransactionInputModel TransactionModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = new SelectList(_context.Accounts.AsNoTracking().ToList(), "Id", "Name");

                return Page();
            }

            var account = _context.Accounts.Find(TransactionModel.AccountId);

            var transaction = new Transaction(TransactionModel.DateTime, account, TransactionModel.Description,
                (TransactionType)Enum.Parse(typeof(TransactionType), TransactionModel.TransactionTypeName), TransactionModel.Amount);

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

    public class TransactionInputModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Account")]
        public long AccountId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string TransactionTypeName { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
