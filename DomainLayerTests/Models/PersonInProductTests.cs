using System.Linq;
using DomainLayer.Enums;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class PersonInProductTests
    {
        [TestMethod]
        public void TestShouldGetPersonFromId()
        {
            // Arrange
            var persons = SamplePersonInProduct.CreatePersonsInProduct();

            // Act
            var result = PersonInProduct.Get(1, Role.Producer);

            // Assert
            var person = persons.SingleOrDefault(result);

            Assert.IsNotNull(person);
            Assert.AreEqual("First Person", person.Name);
        }

        [TestMethod]
        public void TestShouldGetPersonFromIdAndGetNull()
        {
            // Arrange
            var persons = SamplePersonInProduct.CreatePersonsInProduct();

            // Act
            var result = PersonInProduct.Get(2, Role.Producer);

            // Assert
            var person = persons.SingleOrDefault(result);

            Assert.IsNull(person);
        }
    }
}