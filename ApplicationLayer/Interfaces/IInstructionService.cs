using System.Collections.Generic;

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