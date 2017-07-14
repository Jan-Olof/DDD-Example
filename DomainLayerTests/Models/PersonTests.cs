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
        public void TestShouldGetProductFromId()
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
        public void TestShouldGetProductFromName()
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

        private static IPerson CreatePerson()
        {
            return new Person();
        }
    }
}