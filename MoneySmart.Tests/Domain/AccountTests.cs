using System;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain
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
            var sut = new Account(5575, "TestAccount");
            var transactionType = TransactionType.Income;
            var transaction = new Transaction(date, sut, description, transactionType, amount);

            // Act
            sut.AddTransaction(transaction);

            // Assert
            Assert.Equal(10, sut.Balance);
        }
    }
}
