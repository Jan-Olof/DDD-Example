using DomainLayer.Interfaces;

namespace InfrastructureLayer.Dtos
{
    public class ProductDto : IProductProps
    {
        public ProductDto()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}