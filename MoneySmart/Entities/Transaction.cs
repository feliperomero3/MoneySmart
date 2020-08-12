using System;

namespace MoneySmart.Entities
{
    public class Transaction : Entity
    {
        public long TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; private set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public Account Account { get; set; }

        public Transaction(DateTime dateTime, string description, decimal amount, Account account,
            TransactionType transactionType)
        {
            DateTime = dateTime;
            Description = description;
            Amount = amount;
            Account = account;
            TransactionType = transactionType;
        }
    }
}
