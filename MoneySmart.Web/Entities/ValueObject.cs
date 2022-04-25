﻿// Customized from https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/8171649c1b7f41dd2ee4f135433a470daa59e567/CSharpFunctionalExtensions/ValueObject/ValueObject.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.Entities
{
    [Serializable]
    public abstract class ValueObject : IComparable, IComparable<ValueObject>
    {
        private int? _cachedHashCode;

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetUnproxiedType(this) != GetUnproxiedType(obj))
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            if (!_cachedHashCode.HasValue)
            {
                _cachedHashCode = GetEqualityComponents()
                    .Aggregate(1, (current, obj) =>
                    {
                        unchecked
                        {
                            return current * 23 + (obj?.GetHashCode() ?? 0);
                        }
                    });
            }

            return _cachedHashCode.Value;
        }

        public int CompareTo(object obj)
        {
            Type thisType = GetUnproxiedType(this);
            Type otherType = GetUnproxiedType(obj);

            if (thisType != otherType)
                return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);

            var other = (ValueObject)obj;

            object[] components = GetEqualityComponents().ToArray();
            object[] otherComponents = other.GetEqualityComponents().ToArray();

            for (int i = 0; i < components.Length; i++)
            {
                int comparison = CompareComponents(components[i], otherComponents[i]);
                if (comparison != 0)
                    return comparison;
            }

            return 0;
        }

        private int CompareComponents(object object1, object object2)
        {
            if (object1 is null && object2 is null)
                return 0;

            if (object1 is null)
                return -1;

            if (object2 is null)
                return 1;

            var component1 = object1 as IComparable;
            var component2 = object2 as IComparable;

            if (component1 == null || component2 == null)
                throw new InvalidOperationException($"Not all components in {GetUnproxiedType(this)} implement IComparable");

            return component1.CompareTo(component2);
        }

        public int CompareTo(ValueObject other)
        {
            return CompareTo(other as object);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        internal static Type GetUnproxiedType(object obj)
        {
            const string EFCoreProxyPrefix = "Castle.Proxies.";
            const string NHibernateProxyPostfix = "Proxy";

            Type type = obj.GetType();
            string typeString = type.ToString();

            if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
                return type.BaseType;

            return type;
        }
    }
}
