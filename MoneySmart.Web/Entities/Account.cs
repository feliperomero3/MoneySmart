using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.Entities
{
    public class Account : Entity
    {
        public long Number { get; private set; }
        public string Name { get; private set; }
        public decimal Balance => Transactions.Sum(t => t.Amount);
        public ICollection<Transaction> Transactions { get; }

        private Account()
        {
            Transactions = new List<Transaction>();
        }

        public Account(long number, string name) : this()
        {
            Number = number;
            Name = name;
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void EditAccount(Account account)
        {
            Number = account.Number;
            Name = account.Name;
        }
    }
}
