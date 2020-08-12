using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.Entities
{
    public class Account : Entity
    {
        public long AccountId { get; private set; }
        public string Name { get; private set; }
        public string Number { get; private set; }
        public decimal Balance => Transactions.Sum(t => t.Amount);
        public ICollection<Transaction> Transactions { get; private set; }

        private Account()
        {
            Transactions = new List<Transaction>();
        }

        public Account(string name) : this()
        {
            Name = name;
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
