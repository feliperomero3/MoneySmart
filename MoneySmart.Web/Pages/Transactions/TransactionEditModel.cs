using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class TransactionEditModel
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

    public static TransactionEditModel MapFromTransaction(Transaction transaction)
    {
        return new TransactionEditModel
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
