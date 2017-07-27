using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
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
            modelBuilder.Entity<ProductDao>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<ProductDao>()
                .Property(product => product.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<ProductDao>()
                .Property(product => product.Description)
                .HasMaxLength(200);
        }
    }
}