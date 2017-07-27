using System.Collections.Generic;
using DomainLayer.Enums;
using InfrastructureLayer.DataAccess.Daos;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleProductPerson
    {
        public static ProductPerson CreateProductPerson(int productId, int personId, Role role)
        {
            return new ProductPerson
            {
                ProductId = productId,
                PersonId = personId,
                Role = role
            };
        }

        public static List<ProductPerson> CreateProductPersons()
        {
            return new List<ProductPerson>
            {
                CreateProductPerson(1,1,Role.Actor),
                CreateProductPerson(2,1,Role.Actor),
                CreateProductPerson(2,2,Role.Director),
                CreateProductPerson(3,2,Role.Writer)
            };
        }
    }
}