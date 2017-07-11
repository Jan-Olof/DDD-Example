using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SampleInstructions
    {
        public static Product CreateInstruction(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
        {
            return new Product
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<Product> CreateInstructions()
        {
            return new List<Product>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static List<Product> CreateInstructions2()
        {
            return new List<Product>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction.")
            };
        }

        public static List<Product> CreateInstructions3()
        {
            return new List<Product>
            {
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<Product> CreateInstructions4()
        {
            return new List<Product>
            {
                CreateInstruction(1),
                CreateInstruction(2, "SecondInstruction", "This is the second instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstruction(4, "FourthInstruction", "This is the fourth instruction.")
            };
        }

        public static List<Product> CreateInstructionsDuplicate()
        {
            return new List<Product>
            {
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction."),
                CreateInstruction(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}