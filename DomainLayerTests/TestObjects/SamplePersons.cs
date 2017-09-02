using DomainLayer.Enums;
using DomainLayer.Models;
using System.Collections.Generic;

namespace DomainLayerTests.TestObjects
{
    public static class SamplePersons
    {
        public static Person CreatePerson(int id = 0, string firstName = "First", string lastName = "Person")
        {
            return new Person
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public static IList<Person> CreatePersons()
        {
            return new List<Person>
            {
                CreatePerson(1),
                CreatePerson(2, "Second"),
                CreatePerson(3, "Third")
            };
        }

        public static List<Person> CreatePersons2()
        {
            return new List<Person>
            {
                CreatePerson(1),
                CreatePerson(2, "Second")
            };
        }

        public static List<Person> CreatePersons3()
        {
            return new List<Person>
            {
                CreatePerson(3, "Third")
            };
        }

        public static IList<Person> CreatePersons4()
        {
            return new List<Person>
            {
                CreatePerson(1),
                CreatePerson(2, "Second"),
                CreatePerson(3, "Third"),
                CreatePerson(4, "Fourth")
            };
        }

        public static List<Person> CreatePersonsDuplicate()
        {
            return new List<Person>
            {
                CreatePerson(3, "Third"),
                CreatePerson(3, "Third")
            };
        }

        public static Person CreatePersonWithProducts(int id = 0, string firstName = "First", string lastName = "Person", int productId = 0, Role role = Role.Actor)
        {
            return new Person
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Products = CreateProductsInPerson(productId, role)
            };
        }

        private static ProductInPerson CreateProductInPerson(int id = 0, Role role = Role.Actor, string name = "", string description = "")
        {
            return new ProductInPerson
            {
                Id = id,
                Name = name,
                Role = role,
                Description = description
            };
        }

        private static IList<ProductInPerson> CreateProductsInPerson(int id = 0, Role role = Role.Actor, string name = "", string description = "")
        {
            return new List<ProductInPerson>
            {
                CreateProductInPerson(id, role, name, description)
            };
        }
    }
}