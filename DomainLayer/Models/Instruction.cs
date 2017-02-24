using System;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces;

namespace DomainLayer.Models
{
    public class Instruction : IInstruction, IInstructionModel
    {
        public Instruction()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Expression<Func<IInstruction, bool>> Get(int id)
        {
            return instruction => instruction.Id == id;
        }

        public Expression<Func<IInstruction, bool>> Get(string name)
        {
            return instruction => instruction.Name == name;
        }
    }
}