using System.Collections.Generic;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleProductDtos
    {
        public static ProductDto CreateProductDto(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
        {
            return new ProductDto
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<ProductDto> CreateProductDtos()
        {
            return new List<ProductDto>
            {
                CreateProductDto(1),
                CreateProductDto(2, "SecondInstruction", "This is the second instruction."),
                CreateProductDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<ProductDto> CreateProductDtosDuplicate()
        {
            return new List<ProductDto>
            {
                CreateProductDto(3, "ThirdInstruction", "This is the third instruction."),
                CreateProductDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<ProductDto> CreateProductsDtos2()
        {
            return new List<ProductDto>
            {
                CreateProductDto(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}