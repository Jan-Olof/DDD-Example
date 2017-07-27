using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayer.Factories
{
    /// <summary>
    /// Builds persons.
    /// </summary>
    public static class PersonFactory
    {
        /// <summary>
        /// Build a Person from a PersonDao.
        /// </summary>
        public static Person CreatePerson(PersonDao productDao)
        {
            return new Person();
        }

        /// <summary>
        /// Build a PersonDao from a Person.
        /// </summary>
        public static PersonDao CreatePersonDao(Person person)
        {
            return new PersonDao();
        }

        /// <summary>
        /// Build a PersonDto from a Person.
        /// </summary>
        public static PersonDto CreatePersonDto(Person person)
        {
            return new PersonDto();
        }
    }
}