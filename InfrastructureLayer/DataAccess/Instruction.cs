using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.DataAccess
{
    public class Instruction : IInstruction
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}