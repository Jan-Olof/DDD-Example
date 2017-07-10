using System.Collections.Generic;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Interactors
{
    /// <summary>
    /// The instruction interactor interface.
    /// </summary>
    public interface IInstructionInteractor : IBaseInteractor<Instruction>
    {
        /// <summary>
        /// Get instruction by name.
        /// </summary>
        IList<Instruction> Get(string name);

        /// <summary>
        /// Update an instruction.
        /// </summary>
        void Update(Instruction entity, int id);
    }
}