using InfrastructureLayer.DataAccess.Daos;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    /// <summary>
    /// Database configuration for ProductPerson.
    /// </summary>
    public static class ProductPersonConfiguration
    {
        /// <summary>
        /// Database configuration for ProductPerson.
        /// </summary>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPerson>()
                .HasKey(productPerson => new { productPerson.PersonId, productPerson.ProductId, productPerson.Role });

            modelBuilder.Entity<ProductPerson>()
                .HasOne(productPerson => productPerson.Product)
                .WithMany(product => product.ProductPersons)
                .HasForeignKey(productPerson => productPerson.ProductId);

            modelBuilder.Entity<ProductPerson>()
                .HasOne(productPerson => productPerson.Person)
                .WithMany(person => person.ProductPersons)
                .HasForeignKey(productPerson => productPerson.PersonId);
        }
    }
}