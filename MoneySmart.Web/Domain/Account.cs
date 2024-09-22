using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.Domain;

public class Account : Entity
{
    public long Number { get; private set; }

    public string Name { get; private set; }

    public decimal Balance
    {
        get
        {
            var negativeAmount = -Transactions
                .Where(t => t.TransactionType == TransactionType.Expense)
                .Sum(t => t.Amount);

            var positiveAmount = Transactions
                .Where(t => t.TransactionType == TransactionType.Income)
                .Sum(t => t.Amount);

            return negativeAmount + positiveAmount;
        }
    }

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

    public void EditAccount(Account account)
    {
        Number = account.Number;
        Name = account.Name;
    }
}
