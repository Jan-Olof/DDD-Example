using DomainLayer.Interfaces;

namespace InfrastructureLayer.Dtos
{
    public class InstructionDto : IInstructionProps
    {
        public InstructionDto()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}