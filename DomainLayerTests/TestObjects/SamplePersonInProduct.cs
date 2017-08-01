using System.Collections.Generic;
using DomainLayer.Enums;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SamplePersonInProduct
    {
        public static PersonInProduct CreatePersonInProduct(int id = 0, Role role = Role.Actor, string firstName = "First", string lastName = "Person")
        {
            return new PersonInProduct
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Name = $"{firstName} {lastName}",
                Role = role
            };
        }

        public static IList<PersonInProduct> CreatePersonsInProduct()
        {
            return new List<PersonInProduct>
            {
                CreatePersonInProduct(1),
                CreatePersonInProduct(1, Role.Producer),
                CreatePersonInProduct(2, Role.Director, "Second"),
                CreatePersonInProduct(3, Role.Writer, "Third")
            };
        }
    }
}