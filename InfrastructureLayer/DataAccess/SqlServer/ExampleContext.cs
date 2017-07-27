// ReSharper disable ObjectCreationAsStatement
// ReSharper disable UnusedAutoPropertyAccessor.Global

using InfrastructureLayer.DataAccess.Daos;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    /// <summary>
    /// An example DbContext.
    /// </summary>
    public class ExampleContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleContext"/> class.
        /// </summary>
        public ExampleContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleContext"/> class.
        /// </summary>
        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        public DbSet<PersonDao> Persons { get; set; }

        /// <summary>
        /// Gets or sets the productpersons.
        /// </summary>
        public DbSet<ProductPerson> ProductPersons { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public DbSet<ProductDao> Products { get; set; }

        /// <summary>
        /// Override this method to configure the database (and other options) to be used
        /// for this context. This method is called for each instance of the context that is created.
        /// In situations where an instance of Microsoft.EntityFrameworkCore.DbContextOptions
        /// may or may not have been passed to the constructor, you can use Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured
        /// to determine if the options have already been set, and skip some or all of the
        /// logic in Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder).
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            string connection = @"Server=localhost\sql2016;Database=DDD-Example;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention
        /// from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties
        /// on your derived context. The resulting model may be cached and re-used for subsequent
        /// instances of your derived context.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductConfiguration.Configure(modelBuilder);
            PersonConfiguration.Configure(modelBuilder);
            ProductPersonConfiguration.Configure(modelBuilder);
        }
    }
}