using System;

namespace MoneySmart.Entities
{
    public class Transaction : Entity
    {
        public DateTime DateTime { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public Account Account { get; private set; }

        private Transaction()
        {
        }

        public Transaction(DateTime dateTime, string description, decimal amount, Account account,
            TransactionType transactionType) : this()
        {
            DateTime = dateTime;
            Description = description;
            Amount = amount;
            Account = account;
            TransactionType = transactionType;
        }
    }
}
