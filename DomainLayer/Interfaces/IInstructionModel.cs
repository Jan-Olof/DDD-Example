using System;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The instruction model interface.
    /// </summary>
    public interface IInstructionModel : IModel<Instruction>
    {
        /// <summary>
        /// Defines how to get instructions by name.
        /// </summary>
        Expression<Func<Instruction, bool>> Get(string name);
    }
}