using System;

namespace MoneySmart.Domain;

public class TransactionType
{
    public static readonly TransactionType Expense = new(_expense);
    public static readonly TransactionType Income = new(_income);

    private readonly string _type;
    private const string _expense = "Expense";
    private const string _income = "Income";

    private TransactionType(string type) => _type = type;

    public static explicit operator TransactionType(string type)
    {
        if (type == _expense)
        {
            return Expense;
        }
        if (type == _income)
        {
            return Income;
        }

        throw new ArgumentException($"'{nameof(type)}' must be {_expense} or {_income}.", nameof(type));
    }

    public static explicit operator string(TransactionType type) => type.ToString();

    public override string ToString() => _type;
}