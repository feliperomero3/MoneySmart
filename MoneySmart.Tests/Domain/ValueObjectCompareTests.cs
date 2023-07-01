using System;
using System.Collections.Generic;
using System.Linq;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain;

public class ValueObjectCompareTests
{
    [Fact]
    public void Can_sort_simple_value_objects()
    {
        var name3 = new NameSuffix(3);
        var name1 = new NameSuffix(1);
        var name4 = new NameSuffix(4);
        var name2 = new NameSuffix(2);

        var nameSuffixes = new[] { name3, name1, name4, name2 }
            .OrderBy(x => x)
            .ToArray();

        Assert.Collection(nameSuffixes,
            (n) => Assert.Equal(name1, n),
            (n) => Assert.Equal(name2, n),
            (n) => Assert.Equal(name3, n),
            (n) => Assert.Equal(name4, n));
    }

    [Fact]
    public void Can_sort_complex_value_objects()
    {
        var name112 = new Name("111", "111", new NameSuffix(2));
        var name111 = new Name("111", "111", new NameSuffix(1));
        var name221 = new Name("222", "222", new NameSuffix(1));
        var name222 = new Name("222", "222", new NameSuffix(2));
        var name121 = new Name("111", "222", new NameSuffix(1));
        var name122 = new Name("111", "222", new NameSuffix(2));
        var name212 = new Name("222", "111", new NameSuffix(2));
        var name211 = new Name("222", "111", new NameSuffix(1));

        var names = new[] { name112, name111, name221, name222, name121, name122, name212, name211 }
            .OrderBy(x => x)
            .ToArray();

        Assert.Collection(names,
            (n) => Assert.Equal(name111, n),
            (n) => Assert.Equal(name112, n),
            (n) => Assert.Equal(name121, n),
            (n) => Assert.Equal(name122, n),
            (n) => Assert.Equal(name211, n),
            (n) => Assert.Equal(name212, n),
            (n) => Assert.Equal(name221, n),
            (n) => Assert.Equal(name222, n));
    }

    [Fact]
    public void Can_sort_value_objects_containing_nulls()
    {
        var name2 = new Name("1", "1", new NameSuffix(1));
        var name1 = new Name("1", null, new NameSuffix(1));

        var names = new[] { name1, name2, null }
            .OrderBy(x => x)
            .ToArray();

        Assert.Collection(names,
            (n) => Assert.Null(n),
            (n) => Assert.Equal(name1, n),
            (n) => Assert.Equal(name2, n));
    }

    [Fact]
    public void Sorting_value_objects_throws_if_one_of_properties_doesnt_implement_IComparable()
    {
        var vo1 = new ValueObjectWithObjectProperty(new object());
        var vo2 = new ValueObjectWithObjectProperty(new object());

        var func = () => new[] { vo1, vo2 }.OrderBy(x => x).ToArray();

        Assert.Throws<InvalidOperationException>(() => func());
    }

    [Fact]
    public void Compare_value_objects_with_different_types_uses_to_string()
    {
        var number = new Number(1);
        var name = new Name("1", "1", new NameSuffix(1));

        var result = number.CompareTo(name);

        Assert.Equal(-1, result);
    }

    private class ValueObjectWithObjectProperty : ValueObject
    {
        public object SomeProperty { get; }

        public ValueObjectWithObjectProperty(object someProperty)
        {
            SomeProperty = someProperty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SomeProperty;
        }
    }

    private class NameSuffix : ValueObject
    {
        public int Value { get; }

        public NameSuffix(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    private class Name : ValueObject
    {
        public string First { get; }
        public string Last { get; }
        public NameSuffix Suffix { get; }

        public Name(string first, string last, NameSuffix suffix)
        {
            First = first;
            Last = last;
            Suffix = suffix;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
            yield return Suffix;
        }

        public override string ToString()
        {
            return $"{First} {Last} {Suffix}";
        }
    }

    private class Number : ValueObject
    {
        public int Value { get; }

        public Number(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}