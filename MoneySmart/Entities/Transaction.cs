using System;

namespace MoneySmart.Entities
{
    public class Transaction : Entity
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Tipo { get; set; }

        public Account Account { get; set; }
    }
}
