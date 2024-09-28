using System;
using MoneySmart.Domain;

namespace MoneySmart.Tests.Builders;

internal class TransactionBuilder
{
    private readonly TransactionModel _transactionModel = new();

    public TransactionBuilder WithDate(DateTime date)
    {
        _transactionModel.DateTime = date;
        return this;
    }

    public TransactionBuilder WithAmount(decimal amount)
    {
        _transactionModel.Amount = amount;
        return this;
    }

    public TransactionBuilder WithTransactionType(TransactionType type)
    {
        _transactionModel.TransactionType = type.ToString();
        return this;
    }

    public TransactionBuilder WithAccount(Account account)
    {
        _transactionModel.Account = AccountModel.MapToAccountModel(account);
        return this;
    }

    public TransactionBuilder WithDescription(string description)
    {
        _transactionModel.Description = description;
        return this;
    }

    public Transaction Build()
    {
        return _transactionModel.MapToTransaction();
    }

    private class TransactionModel
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public AccountModel Account { get; set; } = new() { Number = 1, Name = $"Account #{DateTime.Now.Ticks}" };

        public string Description { get; set; } = $"Description {DateTime.Now.Ticks}";

        public string TransactionType { get; set; } = "Expense";

        public decimal Amount { get; set; } = 100m;

        public Transaction MapToTransaction()
        {
            return new Transaction(DateTime, Account.MapToAccount(), Description, (TransactionType)TransactionType, Amount);
        }
    }

    private class AccountModel
    {
        public long Number { get; set; }

        public string Name { get; set; }

        public Account MapToAccount()
        {
            return new Account(Number, Name);
        }

        public static AccountModel MapToAccountModel(Account account)
        {
            return new AccountModel
            {
                Number = account.Number,
                Name = account.Name
            };
        }
    }
}