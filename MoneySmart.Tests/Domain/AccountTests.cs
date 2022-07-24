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
            const int Number = 5575;
            const string Name = "TestAccount";

            // Act
            var sut = new Account(Number, Name);

            // Assert
            Assert.NotNull(sut);
        }
    }
}
