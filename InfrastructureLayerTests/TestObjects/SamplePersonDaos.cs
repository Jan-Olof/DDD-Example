using InfrastructureLayer.DataAccess.Daos;
using System.Collections.Generic;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SamplePersonDaos
    {
        public static PersonDao CreatePerson(int id = 0, string firstName = "First", string lastName = "Person")
        {
            return new PersonDao
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public static IList<PersonDao> CreatePersons()
        {
            return new List<PersonDao>
            {
                CreatePerson(1),
                CreatePerson(2, "Second"),
                CreatePerson(3, "Third")
            };
        }

        public static List<PersonDao> CreatePersons2()
        {
            return new List<PersonDao>
            {
                CreatePerson(1),
                CreatePerson(2, "Second")
            };
        }

        public static List<PersonDao> CreatePersons3()
        {
            return new List<PersonDao>
            {
                CreatePerson(3, "Third")
            };
        }

        public static IList<PersonDao> CreatePersons4()
        {
            return new List<PersonDao>
            {
                CreatePerson(1),
                CreatePerson(2, "Second"),
                CreatePerson(3, "Third"),
                CreatePerson(4, "Fourth")
            };
        }

        public static List<PersonDao> CreatePersonsDuplicate()
        {
            return new List<PersonDao>
            {
                CreatePerson(3, "Third"),
                CreatePerson(3, "Third")
            };
        }
    }
}