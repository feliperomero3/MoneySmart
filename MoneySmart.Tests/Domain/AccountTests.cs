using MoneySmart.Domain;
using MoneySmart.Tests.Builders;
using Xunit;

namespace MoneySmart.Tests.Domain
{
    public class AccountTests
    {
        private readonly TransactionBuilder _transactionBuilder = new();

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

            var transaction1 = _transactionBuilder
                .WithAccount(sut)
                .WithAmount(100m)
                .Build();

            var transaction2 = _transactionBuilder
                .WithAccount(sut)
                .WithAmount(300m)
                .Build();

            // Act
            sut.Transactions.Add(transaction1);
            sut.Transactions.Add(transaction2);

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

            var transaction1 = _transactionBuilder
                .WithAccount(sut)
                .WithAmount(100m)
                .WithTransactionType(TransactionType.Income)
                .Build();

            var transaction2 = _transactionBuilder
                .WithAccount(sut)
                .WithAmount(300m)
                .WithTransactionType(TransactionType.Income)
                .Build();

            // Act
            sut.Transactions.Add(transaction1);
            sut.Transactions.Add(transaction2);

            // Assert
            Assert.Equal(400, sut.Balance);
        }

        [Fact]
        public void Account_balance_is_sum_of_expense_and_income_transactions()
        {
            // Arrange
            var sut = new Account(5575, "TestAccount");

            var transaction1 = _transactionBuilder
                .WithAccount(sut)
                .WithTransactionType(TransactionType.Expense)
                .WithAmount(100m)
                .Build();

            var transaction2 = _transactionBuilder
                .WithAccount(sut)
                .WithTransactionType(TransactionType.Income)
                .WithAmount(300m)
                .Build();

            // Act
            sut.Transactions.Add(transaction1);
            sut.Transactions.Add(transaction2);

            // Assert
            Assert.Equal(200, sut.Balance);
        }
    }
}
