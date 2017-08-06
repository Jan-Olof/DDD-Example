using System.Collections.Generic;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleProductDtos
    {
        public static ProductCreate CreateProductCreate(
            string name = "FirstProduct", string description = "This is the first product.")
        {
            return new ProductCreate
            {
                Description = description,
                Name = name
            };
        }

        public static ProductDto CreateProductDto(
            int id = 0, string name = "FirstProduct", string description = "This is the first product.")
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
                CreateProductDto(2, "SecondProduct", "This is the second product."),
                CreateProductDto(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static IList<ProductDto> CreateProductDtosDuplicate()
        {
            return new List<ProductDto>
            {
                CreateProductDto(3, "ThirdProduct", "This is the third product."),
                CreateProductDto(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static IList<ProductDto> CreateProductsDtos2()
        {
            return new List<ProductDto>
            {
                CreateProductDto(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static ProductUpdate CreateProductUpdate(
                                            int id = 0, string name = "FirstProduct", string description = "This is the first product.")
        {
            return new ProductUpdate
            {
                Description = description,
                Id = id,
                Name = name
            };
        }
    }
}