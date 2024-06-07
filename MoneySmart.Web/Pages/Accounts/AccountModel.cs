using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Accounts;

public class AccountModel
{
    [Required]
    public long Number { get; init; }

    [Required]
    public string Name { get; init; }

    public static AccountModel FromAccount(Account account)
    {
        return new AccountModel
        {
            Number = account.Number,
            Name = account.Name
        };
    }
}
