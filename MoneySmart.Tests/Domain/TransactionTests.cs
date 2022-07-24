using System;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain
{
    public class TransactionTests
    {
        [Fact]
        public void Create_transaction()
        {
            // Arrange
            var date = DateTime.Parse("2020-05-08");
            var description = "Payment";
            var amount = 10;
            var account = new Account(5575, "TestAccount");
            var transactionType = TransactionType.Income;

            // Act
            var sut = new Transaction(date, account, description, transactionType, amount);

            // Assert
            Assert.NotNull(sut);
        }
    }
}
