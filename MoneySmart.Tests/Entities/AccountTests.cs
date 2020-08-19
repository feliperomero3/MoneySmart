using System;
using MoneySmart.Entities;
using Xunit;

namespace MoneySmart.Tests.Entities
{
    public class AccountTests
    {
        [Fact]
        public void Account_balance_should_reflect_accumulated_transactions_amount()
        {
            // Arrange
            var date = DateTime.Parse("2020-05-08");
            var description = "Payment received";
            var amount = 10;
            var account = new Account("TestAccount", null);
            var transactionType = TransactionType.Income;

            var newTransaction = new Transaction(date, account, description, transactionType, amount);

            var sut = new Account("Test", null);

            // Act
            sut.AddTransaction(newTransaction);

            // Assert
            Assert.Equal(10, sut.Balance);
        }

        // TODO
        // Account should keep every transaction recorded
        // Account should have a zero balance if no initial amount is provided on creation
        // Account should...

    }
}
