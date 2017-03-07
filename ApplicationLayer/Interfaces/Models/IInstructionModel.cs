using System;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces.Models
{
    /// <summary>
    /// The instruction model interface.
    /// </summary>
    public interface IInstructionModel : IModel<IInstruction>
    {
        /// <summary>
        /// Defines how to get instructions by name.
        /// </summary>
        Expression<Func<IInstruction, bool>> Get(string name);
    }
}