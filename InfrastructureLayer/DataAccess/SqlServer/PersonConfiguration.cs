using InfrastructureLayer.DataAccess.Daos;
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
            modelBuilder.Entity<PersonDao>()
                .HasKey(person => person.Id);

            modelBuilder.Entity<PersonDao>()
                .Property(person => person.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<PersonDao>()
                .Property(person => person.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<PersonDao>()
               .Ignore(person => person.Name);
        }
    }
}