using System;

namespace MoneySmart.Domain;

public class Transaction : Entity
{
    public DateTime DateTime { get; private set; }
    public Account Account { get; private set; }
    public string Description { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    public Transfer Transfer { get; private set; }

    private Transaction()
    {
    }

    public Transaction(DateTime dateTime, Account account, string description,
        TransactionType transactionType, decimal amount, Transfer transfer = null) : this()
    {
        DateTime = dateTime;
        Account = account;
        Description = description;
        TransactionType = transactionType;
        Amount = amount;
        Transfer = transfer;
    }

    public void EditTransaction(Transaction transaction)
    {
        DateTime = transaction.DateTime;
        Account = transaction.Account;
        Description = transaction.Description;
        TransactionType = transaction.TransactionType;
        Amount = transaction.Amount;
    }
}
