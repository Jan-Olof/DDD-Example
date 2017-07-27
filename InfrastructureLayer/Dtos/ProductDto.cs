using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace InfrastructureLayer.Dtos
{
    public class ProductDto : IProductDto
    {
        public ProductDto()
        {
            Description = string.Empty;
            Name = string.Empty;
            Persons = new List<Person>();
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Person> Persons { get; set; }
    }
}