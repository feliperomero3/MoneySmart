using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Transactions;

public class TransactionModel
{
    [DisplayName("Number")]
    public long Id { get; set; }

    [DisplayName("Date")]
    [DisplayFormat(DataFormatString = "{0:g}")]
    public DateTime DateTime { get; set; }

    [DisplayName("Account")]
    public string AccountName { get; set; }

    [DisplayName("Description")]
    public string Description { get; set; }

    [DisplayName("Type")]
    public string TransactionType { get; set; }

    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    public static TransactionModel MapFromTransaction(Transaction transaction)
    {
        return new TransactionModel
        {
            Id = transaction.Id,
            DateTime = transaction.DateTime,
            AccountName = transaction.Account.Name,
            Description = transaction.Description,
            TransactionType = transaction.TransactionType.ToString(),
            Amount = transaction.Amount
        };
    }
}
