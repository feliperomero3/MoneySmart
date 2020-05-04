using System;

namespace MoneySmart.Entities
{
    public class Transaction : Entity
    {
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }

        public Account Account { get; set; }
    }
}
