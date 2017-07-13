using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    /// <summary>
    /// Database configuration for Product.
    /// </summary>
    public static class ProductConfiguration
    {
        /// <summary>
        /// Database configuration for Product.
        /// </summary>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Product>()
                .Property(product => product.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(product => product.Description)
                .HasMaxLength(200);
        }
    }
}