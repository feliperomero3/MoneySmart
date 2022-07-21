// Customized from https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/cf4d6c701cd5e8b7a60a2b2c472aa5893c119800/CSharpFunctionalExtensions.Tests/EntityTests/BasicTests.cs

using System;
using System.Collections.Generic;
using MoneySmart.Domain;
using Xunit;

namespace MoneySmart.Tests.Domain;

public class EntityTests
{
    [Fact]
    public void Derived_entities_are_not_equal()
    {
        var entity1 = new MyEntity(1);
        var entity2 = new MyDerivedEntity(1);

        var equals1 = entity1.Equals(entity2);
        var equals2 = entity1 == entity2;

        Assert.False(equals1);
        Assert.False(equals2);
    }

    [Fact]
    public void Entities_of_different_types_are_not_equal()
    {
        var entity1 = new MyEntity(1);
        var entity2 = new MySecondEntity(1);

        var equals1 = entity1.Equals(entity2);
        var equals2 = entity1 == entity2;

        Assert.False(equals1);
        Assert.False(equals2);
    }

    [Fact]
    public void Two_entities_of_the_same_id_are_equal()
    {
        var entity1 = new MyEntity(1);
        var entity2 = new MyEntity(1);

        var equals1 = entity1.Equals(entity2);
        var equals2 = entity1 == entity2;

        Assert.True(equals1);
        Assert.True(equals2);
    }

    [Fact]
    public void Two_entities_of_different_ids_are_not_equal()
    {
        var entity1 = new MyEntity(1);
        var entity2 = new MyEntity(2);

        var equals1 = entity1.Equals(entity2);
        var equals2 = entity1 == entity2;

        Assert.False(equals1);
        Assert.False(equals2);
    }

    [Fact]
    public void Entities_with_default_ids_are_not_equal()
    {
        var entity1 = new MyEntity(0);
        var entity2 = new MyEntity(0);

        var equals1 = entity1.Equals(entity2);
        var equals2 = entity1 == entity2;

        Assert.False(equals1);
        Assert.False(equals2);
    }

    [Fact]
    public void Comparison_to_null()
    {
        var entity1 = new MyEntity(1);
        MyEntity entity2 = null;
        MyEntity entity3 = null;

        Assert.False(entity1 == null);
        Assert.True(entity2 == null);
        Assert.False(entity1.Equals(null));
        Assert.True(entity2 == entity3);
    }

    public class MyEntity : Entity
    {
        public MyEntity(long id)
            : base(id)
        {
        }
    }

    public class MySecondEntity : Entity
    {
        public MySecondEntity(long id)
            : base(id)
        {
        }
    }

    public class MyDerivedEntity : MyEntity
    {
        public MyDerivedEntity(long id)
            : base(id)
        {
        }
    }

    public class MyId : ValueObject
    {
        public string Value1 { get; }
        public Guid Value2 { get; }

        public MyId(string value1, Guid value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value1;
            yield return Value2;
        }
    }
}