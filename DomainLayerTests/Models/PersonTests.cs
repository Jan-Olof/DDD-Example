// ReSharper disable UnusedMember.Global
using DomainLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void TestShouldGetName()
        {
            // Arrange

            var sut = SamplePersons.CreatePerson();

            // Act
            string result = sut.Name;

            // Assert
            Assert.AreEqual("First Person", result);
        }

        [TestMethod]
        public void TestShouldGetPersonFromId()
        {
            // Arrange
            var persons = SamplePersons.CreatePersons();

            // Act
            var result = Entity.Get<Person>(2);

            // Assert
            var person = persons.SingleOrDefault(result.Compile());

            Assert.IsNotNull(person);
            Assert.AreEqual("Second Person", person.Name);
        }

        [TestMethod]
        public void TestShouldGetPersonFromName()
        {
            // Arrange
            var persons = SamplePersons.CreatePersons();

            // Act
            var result = Entity.Get<Person>("Second Person");

            // Assert
            var person = persons.SingleOrDefault(result.Compile());

            Assert.IsNotNull(person);
            Assert.AreEqual(2, person.Id);
        }

        [TestMethod]
        public void TestShouldMapUpdate()
        {
            // Arrange
            var sut = CreatePerson();

            // Act
            var result = sut.MapUpdate(SamplePersons.CreatePerson(33, "Second", "Human"), SamplePersons.CreatePerson(2));

            // Assert
            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("Second Human", result.Name);
            Assert.AreEqual("Second", result.FirstName);
            Assert.AreEqual("Human", result.LastName);
        }

        [TestMethod]
        public void TestShouldSearchPersonFromName()
        {
            // Arrange
            var allPersons = SamplePersons.CreatePersons();

            // Act
            var result = Entity.Search<Person>("Sec");

            // Assert
            var persons = allPersons.Where(result.Compile());

            Assert.AreEqual(2, persons.Single().Id);
        }

        [TestMethod]
        public void TestShouldSetName()
        {
            // Arrange

            var sut = CreatePerson();

            // Act
            sut.Name = "Super Human";

            // Assert
            Assert.AreEqual("Super", sut.FirstName);
            Assert.AreEqual("Human", sut.LastName);
        }

        [TestMethod]
        public void TestShouldSetNameButHasNoSpace()
        {
            // Arrange

            var sut = CreatePerson();

            // Act
            sut.Name = "SuperHuman";

            // Assert
            Assert.AreEqual(string.Empty, sut.FirstName);
            Assert.AreEqual(string.Empty, sut.LastName);
        }

        [TestMethod]
        public void TestShouldSetNameButHasSpaceFirst()
        {
            // Arrange

            var sut = CreatePerson();

            // Act
            sut.Name = " SuperHuman";

            // Assert
            Assert.AreEqual(string.Empty, sut.FirstName);
            Assert.AreEqual(string.Empty, sut.LastName);
        }

        [TestMethod]
        public void TestShouldSetNameButHasSpaceLast()
        {
            // Arrange

            var sut = CreatePerson();

            // Act
            sut.Name = "SuperHuman ";

            // Assert
            Assert.AreEqual(string.Empty, sut.FirstName);
            Assert.AreEqual(string.Empty, sut.LastName);
        }

        private static IPerson CreatePerson()
        {
            return new Person();
        }
    }
}