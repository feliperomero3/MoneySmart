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
    }
}
