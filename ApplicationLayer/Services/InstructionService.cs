using System.Collections.Generic;
using ApplicationLayer.Interfaces;

namespace ApplicationLayer.Services
{
    public class InstructionService : BaseService<IInstruction, IInstructionModel>, IInstructionService
    {
        public InstructionService(IRepository<IInstruction> repository, IInstructionModel model)
            : base(repository, model)
        {
        }

        public IEnumerable<IInstruction> Get(string name)
        {
            return Repository.Get(Model.Get(name));
        }
    }
}