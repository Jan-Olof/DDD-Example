using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.Configure;
using InfrastructureLayer.Files;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace InfrastructureLayerTests.Files
{
    [TestClass]
    public class FileHandlerTests
    {
        private const string InstructionsFileName = @"..\..\..\Instructions.json";
        private IOptions<Datafile> _dataFile;

        [TestInitialize]
        public void SetUp()
        {
            _dataFile = Substitute.For<IOptions<Datafile>>();
            RestoreFileContent();
        }

        [TestCleanup]
        public void TearDown()
        {
            RestoreFileContent();
        }

        [TestMethod]
        public void TestShouldReadAllInstructions()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = InstructionsFileName });

            var sut = CreateFileHandler();

            // Act
            var result = sut.Read();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("SecondInstruction", result.Single(r => r.Id == 2).Name);
        }

        [TestMethod]
        public void TestShouldWriteInstructionsToFile()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = InstructionsFileName });

            var sut = CreateFileHandler();

            var instructions = sut.Read();
            instructions.Add(SampleProducts.CreateProduct(3, "ThirdInstruction", "This is the third instruction."));

            // Act
            sut.Write(instructions);

            // Assert
            var result = sut.Read();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("SecondInstruction", result.Single(r => r.Id == 2).Name);
            Assert.AreEqual("ThirdInstruction", result.Single(r => r.Id == 3).Name);
        }

        private IFileHandler<IList<Product>> CreateFileHandler()
        {
            return new FileHandler<IList<Product>>(_dataFile, new JsonSerialization());
        }

        private void RestoreFileContent()
        {
            _dataFile.Value.Returns(new Datafile { FileName = InstructionsFileName });
            var sut = CreateFileHandler();
            sut.Write((SampleProducts.CreateProducts2()));
        }
    }
}