//using DomainLayer.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace InfrastructureLayer.DataAccess.SqlServer
//{
//    public class InstructionConfiguration
//    {
//        public InstructionConfiguration(EntityTypeBuilder<Instruction> entityBuilder)
//        {
//            entityBuilder.HasKey(instruction => instruction.Id);
//            entityBuilder.Property(i => i.Name).HasMaxLength(50);
//            entityBuilder.Property(i => i.Description).HasMaxLength(200);
//        }
//    }
//}