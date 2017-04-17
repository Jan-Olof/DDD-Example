using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
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
            RestoreFileContent();
        }

        [TestCleanup]
        public void TearDown()
        {
            RestoreFileContent();
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
            Assert.AreEqual("SecondInstruction", result.Single(r => r.Id == 2).Name);
        }

        [TestMethod]
        public void TestShouldWriteRecipientsToFile()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = @"..\..\..\Instructions.json" });

            var sut = CreateFileHandler();

            var instructions = sut.Get();
            instructions.Add(SampleInstructions.CreateInstruction(3, "ThirdInstruction", "This is the third instruction."));

            // Act
            sut.Write(instructions);

            // Assert
            var result = sut.Get();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("SecondInstruction", result.Single(r => r.Id == 2).Name);
            Assert.AreEqual("ThirdInstruction", result.Single(r => r.Id == 3).Name);
        }

        private IFileHandler<IList<Instruction>> CreateFileHandler()
        {
            return new FileHandler<IList<Instruction>>(_dataFile, new JsonSerialization());
        }

        private void RestoreFileContent()
        {
            _dataFile.Value.Returns(new Datafile { FileName = @"..\..\..\Instructions.json" });
            var sut = CreateFileHandler();
            sut.Write((SampleInstructions.CreateInstructions2()));
        }
    }
}