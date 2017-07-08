using System;
using System.Linq.Expressions;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the Instruction domain model.
    /// </summary>
    public class Instruction : IInstruction, IInstructionModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Instruction"/> class.
        /// </summary>
        public Instruction()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description. A text field that describes the instruction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id. The primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name. This is the name of the instruction.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines how to get instructions by id.
        /// </summary>
        public Expression<Func<IInstruction, bool>> Get(int id)
        {
            return instruction => instruction.Id == id;
        }

        /// <summary>
        /// Defines how to get instructions by name.
        /// </summary>
        public Expression<Func<IInstruction, bool>> Get(string name)
        {
            return instruction => instruction.Name == name;
        }

        /// <summary>
        /// Updates the fields that are supposed to be updated when editing an instruction.
        /// </summary>
        public IInstruction MapUpdate(IInstruction from, IInstruction to)
        {
            to.Name = from.Name;
            to.Description = from.Description;

            return to;
        }
    }
}