using System.Collections.Generic;
using ApplicationLayer.Interfaces.Models;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IInstructionService
    {
        IInstruction Create(IInstruction entity);

        IEnumerable<IInstruction> Get(string name);

        IList<IInstruction> Get();

        IInstruction Get(int id);

        void Update(IInstruction entity, int id);
    }
}