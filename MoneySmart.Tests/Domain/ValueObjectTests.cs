// Customized from https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/cf4d6c701cd5e8b7a60a2b2c472aa5893c119800/CSharpFunctionalExtensions.Tests/ValueObjectTests/BasicTests.cs

using System;
using System.Collections.Generic;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain
{
    public class ValueObjectTests
    {
        [Fact]
        public void Derived_value_objects_are_not_equal()
        {
            var address = new Address("Street", "City");
            var derivedAddress = new DerivedAddress("Country", "Street", "City");

            Assert.False(address.Equals(derivedAddress));
            Assert.False(derivedAddress.Equals(address));
        }

        [Fact]
        public void Two_value_object_of_the_same_content_are_equal()
        {
            var address1 = new Address("Street", "City");
            var address2 = new Address("Street", "City");

            Assert.True(address1.Equals(address2));
            Assert.True(address1.GetHashCode().Equals(address2.GetHashCode()));
        }

        [Fact]
        public void It_is_possible_to_override_default_equality_comparison_behavior()
        {
            var money1 = new Money("usd", 2.2222m);
            var money2 = new Money("USD", 2.22m);

            Assert.True(money1.Equals(money2));
            Assert.True(money1.GetHashCode().Equals(money2.GetHashCode()));
        }

        [Fact]
        public void Comparing_value_objects_of_different_types_returns_false()
        {
            var vo1 = new ValueObject1("1");
            var vo2 = new ValueObject2("1");

            Assert.False(vo1.Equals(vo2));
        }

        [Fact]
        public void Comparing_value_objects_of_different_values_returns_false()
        {
            var emailAddress1 = new ValueObject1("1");
            var emailAddress2 = new ValueObject1("2");

            Assert.False(emailAddress1.Equals(emailAddress2));
        }

        [Fact]
        public void Comparing_value_objects_with_different_collections_returns_false()
        {
            var vo1 = new ValueObjectWithCollection("one", "two");
            var vo2 = new ValueObjectWithCollection("one", "three");

            var result1 = vo1.Equals(vo2);
            var result2 = vo2.Equals(vo1);

            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Comparing_value_objects_with_collections_of_different_size_returns_false()
        {
            var vo1 = new ValueObjectWithCollection("one", "two");
            var vo2 = new ValueObjectWithCollection("one", "two", "three");

            var result1 = vo1.Equals(vo2);
            var result2 = vo2.Equals(vo1);

            Assert.False(result1);
            Assert.False(result2);
        }

        private class ValueObjectWithCollection : ValueObject
        {
            private readonly string[] _components;

            public ValueObjectWithCollection(params string[] components)
            {
                _components = components;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                return _components;
            }
        }

        public class ValueObject1 : ValueObject
        {
            public string Value { get; }

            public ValueObject1(string value)
            {
                Value = value;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }

        public class ValueObject2 : ValueObject
        {
            public string Value { get; }

            public ValueObject2(string value)
            {
                Value = value;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }

        public class EmailAddress : ValueObject
        {
            public string Value { get; }

            public EmailAddress(string value)
            {
                Value = value;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }

        public class Money : ValueObject
        {
            public string Currency { get; }
            public decimal Amount { get; }

            public Money(string currency, decimal amount)
            {
                Currency = currency;
                Amount = amount;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Currency.ToUpper();
                yield return Math.Round(Amount, 2);
            }
        }

        public class Address : ValueObject
        {
            public string Street { get; }
            public string City { get; }

            public Address(string street, string city)
            {
                Street = street;
                City = city;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Street;
                yield return City;
            }
        }

        public class DerivedAddress : Address
        {
            public string Country { get; }

            public DerivedAddress(string country, string street, string city)
                : base(street, city)
            {
                Country = country;
            }
        }
    }
}