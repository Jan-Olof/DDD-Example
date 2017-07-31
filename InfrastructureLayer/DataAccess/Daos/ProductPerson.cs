// ReSharper disable NonReadonlyMemberInGetHashCode

using System;
using DomainLayer.Enums;

namespace InfrastructureLayer.DataAccess.Daos
{
    /// <summary>
    /// Handle many-to-many relations between product and person.
    /// </summary>
    public class ProductPerson : IEquatable<ProductPerson>
    {
        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public PersonDao Person { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public ProductDao Product { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(ProductPerson other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return PersonId == other.PersonId && ProductId == other.ProductId && Role == other.Role;
        }

        /// <summary>
        /// NOTE: ReSharper disable NonReadonlyMemberInGetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return PersonId.GetHashCode() + ProductId.GetHashCode() + Role.GetHashCode();
        }
    }
}