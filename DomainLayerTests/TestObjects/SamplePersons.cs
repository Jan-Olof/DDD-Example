using System.Collections.Generic;
using DomainLayer.Models;

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
    }
}