using System;

namespace MoneySmart.Entities
{
    public class Transaction : Entity
    {
        public Transaction(DateTime dateTime, string description, double amount, Account account,
            TransactionType transactionType)
        {
            DateTime = dateTime;
            Description = description;
            Amount = amount;
            Account = account;
            TransactionType = transactionType;
        }

        public DateTime DateTime { get; set; }
        public string Description { get; private set; }
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public Account Account { get; set; }
    }
}
