using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using DomainLayer.Interfaces;

namespace ApplicationLayer.Services
{
    public class InstructionService : BaseService<IInstruction, IInstructionModel>
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