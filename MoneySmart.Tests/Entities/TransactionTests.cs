using System;
using MoneySmart.Entities;
using Xunit;

namespace MoneySmart.Tests.Entities
{
    public class TransactionTests
    {
        [Fact]
        public void Can_create_an_income_transaction()
        {
            // Arrange
            var date = DateTime.Parse("2020-05-08");
            var description = "Payment";
            var amount = 10;
            var account = new Account("TestAccount", "5575");
            var transactionType = TransactionType.Income;

            // Act
            var sut = new Transaction(date, account, description, transactionType, amount);

            // Assert
            Assert.Equal(TransactionType.Income, sut.TransactionType);
        }

        [Fact]
        public void Can_create_an_expense_transaction()
        {
            // Arrange
            var date = DateTime.Parse("2020-05-08");
            var description = "Payment";
            var amount = 10;
            var account = new Account("TestAccount", "5575");
            var transactionType = TransactionType.Expense;

            // Act
            var sut = new Transaction(date, account, description, transactionType, amount);

            // Assert
            Assert.Equal(TransactionType.Expense, sut.TransactionType);
        }
    }
}
