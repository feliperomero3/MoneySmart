using System.Collections.Generic;

namespace MoneySmart.Domain;

public class Transfer : Entity
{
    public string Notes { get; set; }
    public ICollection<Transaction> Transactions { get; } = new HashSet<Transaction>();
}