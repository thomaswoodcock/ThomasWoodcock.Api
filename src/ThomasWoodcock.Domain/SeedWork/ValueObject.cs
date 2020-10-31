using System.Collections.Generic;
using System.Linq;

namespace ThomasWoodcock.Domain.SeedWork
{
    // https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/ValueObject.cs
    /// <summary>
    /// Allows a class to act as a value object.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Overrides the logic of the '==' operator for comparing two ValueObjects.
        /// </summary>
        /// <remarks>
        /// If one ValueObject is <c>null</c> but the other is not, they are deemed unequal.
        /// Otherwise, if they are both null, or <see cref="ValueObject.Equals(object)" />
        /// returns <c>true</c>, they are deemed equal.
        /// </remarks>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right!);
        }

        /// <summary>
        /// Overrides the logic of the '!=' operator for comparing two ValueObjects.
        /// </summary>
        /// See '=' operator override for the logic that determines whether two
        /// ValueObjects are equal.
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the properties of the ValueObject that are used to determine equality.
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Overrides the logic of the <see cref="object.Equals(object)" /> method for
        /// comparing two ValueObjects.
        /// </summary>
        /// <remarks>
        /// If the given ValueObject is null or of a different type, it is deemed unequal.
        /// Otherwise, <see cref="ValueObject.GetEqualityComponents" /> is used to compare
        /// the properties of the two ValueObjects.
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ValueObject other))
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return this.GetEqualityComponents()
                       .SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Overrides the logic of the <see cref="object.GetHashCode" /> method.
        /// </summary>
        /// <remarks>
        /// Uses the result of <see cref="ValueObject.GetEqualityComponents" />.
        /// </remarks>
        public override int GetHashCode()
        {
            return this.GetEqualityComponents()
                       .Select(x => x != null ? x.GetHashCode() : 0)
                       .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Creates a copy of the ValueObject.
        /// </summary>
        public ValueObject? GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}
