// ReSharper disable  ObjectCreationAsStatement
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    public class ExampleContext : DbContext
    {
        public ExampleContext()
        {
        }

        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<Instruction> Instructions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost\sql2016;Database=EfExampleDatabase;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instruction>()
                .HasKey(instruction => instruction.Id);

            modelBuilder.Entity<Instruction>()
                .Property(instruction => instruction.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Instruction>()
                .Property(instruction => instruction.Description)
                .HasMaxLength(200);

            //new InstructionConfiguration(modelBuilder.Entity<Instruction>());
        }
    }
}