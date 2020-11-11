using System;

namespace ThomasWoodcock.Domain.SeedWork
{
    // https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Entity.cs
    // https://github.com/NetDevPack/NetDevPack/blob/master/src/NetDevPack/Domain/Entity.cs
    /// <summary>
    /// Allows a class to act as a domain entity.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Initializes a new instance of the Entity class using the given <paramref name="id" />.
        /// </summary>
        /// <param name="id">
        /// The GUID ID to associate with the Entity.
        /// </param>
        protected Entity(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the Entity's ID.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Overrides the logic of the '==' operator for comparing two Entities.
        /// </summary>
        /// <remarks>
        /// If one Entity is <c>null</c> but the other is not, they are deemed unequal.
        /// Otherwise, if they are both null, or <see cref="Entity.Equals(object)" />
        /// returns <c>true</c>, they are deemed equal.
        /// </remarks>
        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right!);
        }

        /// <summary>
        /// Overrides the logic of the '!=' operator for comparing two Entities.
        /// </summary>
        /// See '=' operator override for the logic that determines whether two
        /// Entities are equal.
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Overrides the logic of the <see cref="object.Equals(object)" /> method for
        /// comparing two Entities.
        /// </summary>
        /// <remarks>
        /// If the given Entity is null, not an Entity, or of a different type, it is
        /// deemed unequal. Otherwise, its ID is compared for equality.
        /// </remarks>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Entity other)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return other.Id == this.Id;
        }

        /// <summary>
        /// Overrides the logic of the <see cref="object.GetHashCode" /> method.
        /// </summary>
        /// <remarks>
        /// Uses the Entity's type and ID.
        /// </remarks>
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + this.Id.GetHashCode();
        }

        /// <summary>
        /// Overrides the logic of the <see cref="object.ToString" /> method.
        /// </summary>
        /// <example>
        /// MyEntity [Id=276dfdf1-ed68-4861-bea8-0ecaa9d8ecb0]
        /// </example>
        public override string ToString()
        {
            return $"{GetType().Name} [Id={this.Id}]";
        }
    }
}
