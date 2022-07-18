using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain;

public class TransactionTypeTests
{
    [Fact]
    public void TransactionType_does_expected_equality()
    {
        var type1 = TransactionType.Expense;
        var type2 = TransactionType.Expense;

        Assert.Equal(type1, type2);
    }

    [Fact]
    public void TransactionType_does_expected_non_equality()
    {
        var type1 = TransactionType.Expense;
        var type2 = TransactionType.Income;

        Assert.NotEqual(type1, type2);
    }

    [Fact]
    public void TransactionType_converts_to_string()
    {
        var transactionType = TransactionType.Expense;

        var type = (string)transactionType;

        Assert.Equal("Expense", type);
    }

    [Fact]
    public void TransactionType_converts_from_string()
    {
        var type = "Income";

        var transactionType = (TransactionType)type;

        Assert.Equal(TransactionType.Income, transactionType);
    }
}