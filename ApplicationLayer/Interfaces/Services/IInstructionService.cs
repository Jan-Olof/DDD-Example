using System.Collections.Generic;
using DomainLayer.Interfaces;

namespace ApplicationLayer.Interfaces.Services
{
    /// <summary>
    /// The instruction service interface.
    /// </summary>
    public interface IInstructionService : IBaseService<IInstruction>
    {
        /// <summary>
        /// Get instruction by name.
        /// </summary>
        IList<IInstruction> Get(string name);

        /// <summary>
        /// Update an instruction.
        /// </summary>
        void Update(IInstruction entity, int id);
    }
}