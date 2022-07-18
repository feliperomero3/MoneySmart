// Customized from https://github.com/vkhorikov/DddAndEFCore/blob/90bf08dcaa8f9ab000713060effa8a7f46a303e1/src/App/Entity.cs

using System;

namespace MoneySmart.Domain
{
    public abstract class Entity
    {
        public long Id { get; }

        protected Entity()
        {
        }

        protected Entity(long id)
            : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            const string EfCoreProxyPrefix = "Castle.Proxies.";

            var type = GetType();

            return type.ToString().Contains(EfCoreProxyPrefix) ? type.BaseType : type;
        }
    }
}
