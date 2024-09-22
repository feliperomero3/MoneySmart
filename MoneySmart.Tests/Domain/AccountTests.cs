using System;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain
{
    public class AccountTests
    {
        [Fact]
        public void Create_account()
        {
            // Arrange
            const int number = 5575;
            const string name = "TestAccount";

            // Act
            var sut = new Account(number, name);

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void Edit_account()
        {
            // Arrange
            const int number = 5575;
            const string name = "TestAccount";
            var sut = new Account(number, name);

            const int newNumber = 5576;
            const string newName = "NewTestAccount";
            var newAccount = new Account(newNumber, newName);

            // Act
            sut.EditAccount(newAccount);

            // Assert
            Assert.Equal(newNumber, sut.Number);
            Assert.Equal(newName, sut.Name);
        }

        [Fact]
        public void Account_balance_is_sum_of_transactions()
        {
            // Arrange
            var sut = new Account(5575, "TestAccount");

            // Act
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Expense,  100m));
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Expense,  300m));

            // Assert
            Assert.Equal(-400, sut.Balance);
        }

        [Fact]
        public void Account_balance_is_zero_when_no_transactions()
        {
            // Arrange
            var sut = new Account(5575, "TestAccount");

            // Assert
            Assert.Equal(0, sut.Balance);
        }

        [Fact]
        public void Account_balance_is_sum_of_income_transactions()
        {
            // Arrange
            var sut = new Account(5575, "TestAccount");

            // Act
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Income, 100m));
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Income, 300m));

            // Assert
            Assert.Equal(400, sut.Balance);
        }

        [Fact]
        public void Account_balance_is_sum_of_expense_and_income_transactions()
        {
            // Arrange
            var sut = new Account(5575, "TestAccount");

            // Act
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Expense, 100m));
            sut.Transactions.Add(TransactionBuilder.GetTransaction(sut, TransactionType.Income, 300m));

            // Assert
            Assert.Equal(200, sut.Balance);
        }
    }

    internal static class TransactionBuilder
    {
        public static Transaction GetTransaction(Account account, TransactionType type, decimal amount)
        {
            return new Transaction(DateTime.Now, account, $"Transaction {DateTime.Now}", type, amount);
        }
    }
}
