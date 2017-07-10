using System.Linq;
using ApplicationLayer.Services;
using DomainLayerTests.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Utilities.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces;
using DomainLayer.Models;

// ReSharper disable UnusedMember.Global

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class InstructionServiceTests
    {
        private ILogger<InstructionService> _logger;
        private IInstruction _model;
        private IRepository<Instruction> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<Instruction>>();
            _model = Substitute.For<IInstruction>();
            _logger = Substitute.For<ILogger<InstructionService>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldCreateInstruction()
        {
            // Arrange
            _repository.Insert(SampleInstructions.CreateInstruction())
                .ReturnsForAnyArgs(SampleInstructions.CreateInstruction(1));

            var sut = CreateInstructionService();

            // Act
            var result = sut.Create(SampleInstructions.CreateInstruction());

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void TestShouldGetAllInstructions()
        {
            // Arrange
            _repository.Get()
                .Returns(SampleInstructions.CreateInstructions());

            var sut = CreateInstructionService();

            // Act
            var result = sut.Get();

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestShouldGetInstructionFromId()
        {
            // Arrange
            _model.Get(3)
                .Returns(i => i.Id == 3);

            _repository.Get(i => i.Id == 3)
                .ReturnsForAnyArgs(SampleInstructions.CreateInstructions3());

            var sut = CreateInstructionService();

            // Act
            var result = sut.Get(3);

            // Assert
            Assert.AreEqual("ThirdInstruction", result.Name);
        }

        [TestMethod]
        public void TestShouldGetInstructionFromIdAndFindsDuplicates()
        {
            // Arrange
            _model.Get(3)
                .Returns(i => i.Id == 3);

            _repository.Get(i => i.Id == 3)
                .ReturnsForAnyArgs(SampleInstructions.CreateInstructionsDuplicate());

            var sut = CreateInstructionService();

            // Act and assert
            Assert.ThrowsException<TooManyFoundException>(() => sut.Get(3));
        }

        [TestMethod]
        public void TestShouldGetInstructionFromName()
        {
            // Arrange
            _model.Get("ThirdInstruction")
                .Returns(i => i.Name == "ThirdInstruction");

            _repository.Get(i => i.Name == "ThirdInstruction")
                .ReturnsForAnyArgs(SampleInstructions.CreateInstructions3());

            var sut = CreateInstructionService();

            // Act
            var result = sut.Get("ThirdInstruction");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldUpdateInstructionFromId()
        {
            // Arrange
            _model.Get(3).Returns(i => i.Id == 3);

            _repository.Update(SampleInstructions.CreateInstruction(), i => i.Id == 3);

            var sut = CreateInstructionService();

            // Act
            sut.Update(SampleInstructions.CreateInstruction(3), 3);

            // Assert
            Assert.IsTrue(true);
        }

        private InstructionService CreateInstructionService()
        {
            return new InstructionService(_repository, _model, _logger);
        }
    }
}