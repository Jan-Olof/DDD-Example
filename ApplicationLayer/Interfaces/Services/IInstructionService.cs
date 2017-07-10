using System.Collections.Generic;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Services
{
    /// <summary>
    /// The instruction service interface.
    /// </summary>
    public interface IInstructionService : IBaseService<Instruction>
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