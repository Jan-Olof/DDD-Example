using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using InfrastructureLayer.Configure;
using InfrastructureLayer.Files;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace AdminWebApiTests
{
    [TestClass]
    public class FileHandlerTests
    {
        private IOptions<Datafile> _dataFile;

        [TestInitialize]
        public void SetUp()
        {
            _dataFile = Substitute.For<IOptions<Datafile>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetAllRecipients()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = @"..\..\..\Instructions.json" });

            var sut = CreateFileHandler();

            // Act
            var result = sut.Get();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Second Instruction", result.Single(r => r.Id == 2).Name);
        }

        private IFileHandler<IList<Instruction>> CreateFileHandler()
        {
            return new FileHandler<IList<Instruction>>(_dataFile, new JsonSerialization());
        }
    }
}