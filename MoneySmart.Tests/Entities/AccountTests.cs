using System;
using MoneySmart.Entities;
using Xunit;

namespace MoneySmart.Tests.Entities
{
    public class AccountTests
    {
        [Fact]
        public void Account_balance_should_reflect_acumulated_transactions_amount()
        {
            // Arrange
            var date = DateTime.Parse("2020-05-08");
            var description = "Payment received";
            var amount = 10;
            var account = new Account("TestAccount");
            var transactionType = TransactionType.Income;

            var newTransaction = new Transaction(date, description, amount, account, transactionType);

            var sut = new Account("Test");

            // Act
            sut.AddTransaction(newTransaction);

            // Assert
            Assert.Equal(10, sut.Balance);
        }

        // TODO
        // Account should keep every transaction recorded
        // Account should have a zero balance if not provided on creation
        // Account should...

    }
}
