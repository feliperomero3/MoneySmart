using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class TransactionEditModel
{
    [Required]
    [DisplayName("Number")]
    public long Id { get; set; }

    [Required]
    [DisplayName("Date")]
    public DateTime DateTime { get; set; }

    [Required]
    [DisplayName("Account")]
    public long AccountId { get; set; }

    [Required]
    [DisplayName("Description")]
    public string Description { get; set; }

    [Required]
    [DisplayName("Type")]
    public string TransactionType { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    public static TransactionEditModel MapFromTransaction(Transaction transaction)
    {
        return new TransactionEditModel
        {
            Id = transaction.Id,
            DateTime = transaction.DateTime,
            AccountId = transaction.Account.Id,
            Description = transaction.Description,
            TransactionType = transaction.TransactionType.ToString(),
            Amount = transaction.Amount
        };
    }

    public Transaction MapToTransaction(Account account)
    {
        return new Transaction(DateTime, account, Description,
            (TransactionType)TransactionType, Amount);
    }
}
