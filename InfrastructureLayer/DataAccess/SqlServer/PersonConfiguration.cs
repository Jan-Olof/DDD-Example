using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    /// <summary>
    /// Database configuration for Person.
    /// </summary>
    public static class PersonConfiguration
    {
        /// <summary>
        /// Database configuration for Person.
        /// </summary>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(person => person.Id);

            modelBuilder.Entity<Person>()
                .Ignore(person => person.Name);

            modelBuilder.Entity<Person>()
                .Property(product => product.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(product => product.LastName)
                .HasMaxLength(50);
        }
    }
}