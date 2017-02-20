using System.Collections.Generic;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using DomainLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class InstructionServiceTests
    {
        private IRepository<Instruction> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<Instruction>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetAllInstructions()
        {
            // Arrange
            _repository.Get().Returns(new List<Instruction>());

            var sut = CreateInstructionService();

            // Act
            var result = sut.GetInstructions();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        private InstructionService<Instruction> CreateInstructionService()
        {
            return new InstructionService<Instruction>(_repository);
        }
    }
}