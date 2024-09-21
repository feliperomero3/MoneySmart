using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Accounts;

public class AccountModel
{
    public long Number { get; init; }

    public string Name { get; init; }

    [DataType(DataType.Currency)]
    public decimal Balance { get; private set; }

    public static AccountModel MapFromAccount(Account account)
    {
        return new AccountModel
        {
            Number = account.Number,
            Name = account.Name,
            Balance = account.Balance
        };
    }
}
