// ReSharper disable UnusedMember.Global
using System.Linq;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var sut = CreatePerson();

            // Act
            var result = sut.Get(2);

            // Assert
            var person = SamplePersons.CreatePersons().SingleOrDefault(result.Compile());

            Assert.AreEqual("Second Person", person.Name);
        }

        [TestMethod]
        public void TestShouldGetPersonFromName()
        {
            // Arrange

            var sut = CreatePerson();

            // Act
            var result = sut.Get("Second Person");

            // Assert
            var person = SamplePersons.CreatePersons().SingleOrDefault(result.Compile());

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
            var sut = CreatePerson();

            // Act
            var result = sut.Search("Sec");

            // Assert
            var persons = SamplePersons.CreatePersons().Where(result.Compile());

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