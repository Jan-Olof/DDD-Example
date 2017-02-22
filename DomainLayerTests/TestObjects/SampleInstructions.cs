using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SampleInstructions
    {
        public static IInstruction CreateInstruction(
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
                (Instruction)CreateInstruction(1),
                (Instruction)CreateInstruction(2, "SecondInstruction", "This is the second instruction."),
                (Instruction)CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<IInstruction> CreateInstructions3()
        {
            return new List<IInstruction>
            {
                (Instruction)CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<IInstruction> CreateInstructionsDuplicate()
        {
            return new List<IInstruction>
            {
                (Instruction)CreateInstruction(3, "ThirdInstruction", "This is the third instruction."),
                (Instruction)CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}