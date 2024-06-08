using System;
using System.Collections.Generic;

namespace MoneySmart.Domain;

public class TransactionType
{
    public static readonly TransactionType Expense = new(ExpenseValue);
    public static readonly TransactionType Income = new(IncomeValue);

    public static IEnumerable<TransactionType> Values
    {
        get
        {
            yield return Expense;
            yield return Income;
        }
    }

    private readonly string _type;
    private const string ExpenseValue = "Expense";
    private const string IncomeValue = "Income";

    private TransactionType(string type) => _type = type;

    public static explicit operator TransactionType(string type)
    {
        if (type == ExpenseValue)
        {
            return Expense;
        }
        if (type == IncomeValue)
        {
            return Income;
        }

        throw new ArgumentException($"'{nameof(type)}' must be {ExpenseValue} or {IncomeValue}.", nameof(type));
    }

    public static explicit operator string(TransactionType type) => type.ToString();

    public override string ToString() => _type;
}