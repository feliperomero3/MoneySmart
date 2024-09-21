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
    }
}
