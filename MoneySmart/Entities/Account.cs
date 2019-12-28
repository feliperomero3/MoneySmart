using System.Collections.Generic;

namespace MoneySmart.Entities
{
    public class Account : Entity
    {
        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public string Name { get; set; }
        public double Balance { get; set; }

        public ICollection<Transaction> Transactions { get; }

    }
}
