using System.Collections.Generic;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleInstructionDtos
    {
        public static ProductDto CreateInstructionDto(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
        {
            return new ProductDto
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<ProductDto> CreateInstructionDtos()
        {
            return new List<ProductDto>
            {
                CreateInstructionDto(1),
                CreateInstructionDto(2, "SecondInstruction", "This is the second instruction."),
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<ProductDto> CreateInstructionDtosDuplicate()
        {
            return new List<ProductDto>
            {
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<ProductDto> CreateInstructionsDtos2()
        {
            return new List<ProductDto>
            {
                CreateInstructionDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}