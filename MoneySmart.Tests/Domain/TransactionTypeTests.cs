using System;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain;

public class TransactionTypeTests
{
    [Fact]
    public void TransactionType_throws_when_casting_parameter_is_null_or_empty_string()
    {
        var func = () => (TransactionType)string.Empty;

        Assert.Throws<ArgumentException>(func);
    }

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
    public void TransactionType_casting_to_string()
    {
        var transactionType = TransactionType.Expense;

        var type = (string)transactionType;

        Assert.Equal("Expense", type);
    }

    [Fact]
    public void TransactionType_casting_from_string()
    {
        var type = "Income";

        var transactionType = (TransactionType)type;

        Assert.Equal(TransactionType.Income, transactionType);
    }
}