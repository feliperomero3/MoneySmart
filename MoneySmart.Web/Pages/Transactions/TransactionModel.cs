using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class TransactionModel
{
    [DisplayName("Number")]
    public long Id { get; init; }

    [DisplayName("Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime DateTime { get; init; }

    [DisplayName("Account")]
    public string AccountName { get; init; }

    [DisplayName("Description")]
    public string Description { get; init; }

    [DisplayName("Type")]
    public string TransactionType { get; init; }

    [DataType(DataType.Currency)]
    public decimal Amount { get; init; }

    [DataType(DataType.MultilineText)]
    public string Note { get; init; }

    public static TransactionModel MapFromTransaction(Transaction transaction)
    {
        return new TransactionModel
        {
            Id = transaction.Id,
            DateTime = transaction.DateTime,
            AccountName = transaction.Account.Name,
            Description = transaction.Description,
            TransactionType = transaction.TransactionType.ToString(),
            Amount = transaction.Amount,
            Note = transaction.Note
        };
    }
}
