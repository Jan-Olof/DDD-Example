using System.Linq;
using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedMember.Global

namespace DomainLayerTests.Models
{
    [TestClass]
    public class InstructionTests
    {
        [TestMethod]
        public void TestShouldGetAnInstructionFromId()
        {
            // Arrange

            var sut = CreateInstruction();

            // Act
            var result = sut.Get(2);

            // Assert
            var instruction = SampleInstructions.CreateInstructions().SingleOrDefault(result.Compile());

            Assert.AreEqual("SecondInstruction", instruction.Name);
        }

        [TestMethod]
        public void TestShouldGetAnInstructionsFromName()
        {
            // Arrange

            var sut = CreateInstruction();

            // Act
            var result = sut.Get("SecondInstruction");

            // Assert
            var instruction = SampleInstructions.CreateInstructions().SingleOrDefault(result.Compile());

            Assert.AreEqual(2, instruction.Id);
        }

        private static IInstructionModel CreateInstruction()
        {
            return new Instruction();
        }
    }
}