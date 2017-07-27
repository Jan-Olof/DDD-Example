using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;

namespace InfrastructureLayer.Factories
{
    /// <summary>
    /// Builds ProductPerson objects.
    /// </summary>
    public static class ProductPersonFactory
    {
        /// <summary>
        /// Build a PersonInProduct from a ProductPerson.
        /// </summary>
        public static PersonInProduct CreatePersonInProduct(ProductPerson productPerson)
        {
            return new PersonInProduct
            {
                FirstName = productPerson.Person.FirstName,
                LastName = productPerson.Person.LastName,
                Name = productPerson.Person.Name,
                Id = productPerson.Person.Id,
                Role = productPerson.Role
            };
        }

        /// <summary>
        /// Build a ProductPerson from a PersonInProduct.
        /// </summary>
        public static ProductPerson CreateProductPerson(PersonInProduct personInProduct, int productId)
        {
            return new ProductPerson
            {
                PersonId = personInProduct.Id,
                ProductId = productId,
                Role = personInProduct.Role
            };
        }
    }
}