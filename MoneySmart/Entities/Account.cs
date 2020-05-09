using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.Entities
{
    public class Account : Entity
    {
        public Account(string name) : this()
        {
            Name = name;
        }

        private Account()
        {
            Transactions = new List<Transaction>();
        }

        public double Balance
        {
            get => Transactions.Sum(t => t.Amount);
            private set { }
        }

        public string Name { get; }

        public ICollection<Transaction> Transactions { get; }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
