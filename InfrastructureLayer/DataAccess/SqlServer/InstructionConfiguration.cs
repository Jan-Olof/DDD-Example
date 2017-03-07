using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess.SqlServer
{
    /// <summary>
    /// Database configuration for Instruction.
    /// </summary>
    public static class InstructionConfiguration
    {
        /// <summary>
        /// Database configuration for Instruction.
        /// </summary>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instruction>()
                .HasKey(instruction => instruction.Id);

            modelBuilder.Entity<Instruction>()
                .Property(instruction => instruction.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Instruction>()
                .Property(instruction => instruction.Description)
                .HasMaxLength(200);
        }
    }
}