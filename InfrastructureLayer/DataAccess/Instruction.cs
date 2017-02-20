using DomainLayer.Interfaces;

namespace InfrastructureLayer.DataAccess
{
    public class Instruction : IInstruction
    {
        public Instruction()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}