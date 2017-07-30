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
        /// Build a ProductInPerson from a ProductPerson.
        /// </summary>
        public static ProductInPerson CreateProductInPerson(ProductPerson productPerson)
        {
            return new ProductInPerson
            {
                Id = productPerson.Product.Id,
                Name = productPerson.Product.Name,
                Description = productPerson.Product.Description,
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

        /// <summary>
        /// Build a ProductPerson from a PersonInProduct.
        /// </summary>
        public static ProductPerson CreateProductPerson(ProductInPerson productInPerson, int personId)
        {
            return new ProductPerson
            {
                PersonId = productInPerson.Id,
                ProductId = personId,
                Role = productInPerson.Role
            };
        }
    }
}