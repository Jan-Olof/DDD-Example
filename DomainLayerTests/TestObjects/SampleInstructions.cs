using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SampleInstructions
    {
        public static Instruction CreateInstruction(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
        {
            return new Instruction
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<IInstruction> CreateInstructions()
        {
            return new List<IInstruction>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static List<Instruction> CreateInstructions2()
        {
            return new List<Instruction>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction.")
            };
        }

        public static List<Instruction> CreateInstructions3()
        {
            return new List<Instruction>
            {
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<IInstruction> CreateInstructions4()
        {
            return new List<IInstruction>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstruction(4, "FourthInstruction", "This is the fourth instruction.")
            };
        }

        public static List<Instruction> CreateInstructionsDuplicate()
        {
            return new List<Instruction>
            {
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}