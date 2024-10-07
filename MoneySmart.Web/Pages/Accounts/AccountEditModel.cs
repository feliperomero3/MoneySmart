using System.ComponentModel.DataAnnotations;
using MoneySmart.Domain;

namespace MoneySmart.Pages.Accounts;

public class AccountEditModel
{
    [Required]
    public long Number { get; init; }

    [Required]
    public string Name { get; init; }

    public static AccountEditModel MapFromAccount(Account account)
    {
        return new AccountEditModel
        {
            Number = account.Number,
            Name = account.Name
        };
    }

    public Account MapToAccount()
    {
        return new Account(Number, Name);
    }
}
