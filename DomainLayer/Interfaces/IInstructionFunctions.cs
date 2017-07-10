using System;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The instruction model functions interface.
    /// </summary>
    public interface IInstructionFunctions : IFunctions<Instruction>
    {
        /// <summary>
        /// Defines how to get instructions by name.
        /// </summary>
        Expression<Func<Instruction, bool>> Get(string name);
    }
}