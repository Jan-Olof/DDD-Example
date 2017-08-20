using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CLI
{
    /// <summary>
    /// This class is used (by convention) by EF Core to access the application's service provider at design time.
    /// </summary>
    public class ExampleContextFactory : IDesignTimeDbContextFactory<ExampleContext>
    {
        /// <summary>
        /// This method is used (by convention) by EF Core to access the application's service provider at design time.
        /// </summary>
        public ExampleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();
            optionsBuilder.UseSqlServer(@"Server=localhost\sql2016;Database=DDD-Example;Trusted_Connection=True;");

            return new ExampleContext(optionsBuilder.Options);
        }
    }
}