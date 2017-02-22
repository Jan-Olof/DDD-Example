using System.Collections.Generic;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleInstructionDtos
    {
        public static InstructionDto CreateInstructionDto(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
        {
            return new InstructionDto
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<InstructionDto> CreateInstructionDtos()
        {
            return new List<InstructionDto>
            {
                CreateInstructionDto(1),
                CreateInstructionDto(2, "SecondInstruction", "This is the second instruction."),
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<InstructionDto> CreateInstructionDtosDuplicate()
        {
            return new List<InstructionDto>
            {
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<InstructionDto> CreateInstructionsDtos2()
        {
            return new List<InstructionDto>
            {
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}