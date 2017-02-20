using System.Linq;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using DomainLayer.Interfaces;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

// ReSharper disable UnusedMember.Global

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class InstructionServiceTests
    {
        private IInstructionModel _model;
        private IRepository<IInstruction> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<IInstruction>>();
            _model = Substitute.For<IInstructionModel>();
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

        private InstructionService CreateInstructionService()
        {
            return new InstructionService(_repository, _model);
        }
    }
}