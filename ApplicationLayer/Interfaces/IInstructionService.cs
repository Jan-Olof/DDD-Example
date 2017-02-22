using System.Collections.Generic;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces
{
    public interface IInstructionService
    {
        IInstruction Create(IInstruction entity);

        IEnumerable<IInstruction> Get(string name);

        IList<IInstruction> Get();

        IInstruction Get(int id);
    }
}