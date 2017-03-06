using System;
using System.Collections.Generic;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Utilities.Enums;

namespace ApplicationLayer.Services
{
    public class InstructionService : BaseService<IInstruction, IInstructionModel>, IInstructionService
    {
        public InstructionService(IRepository<IInstruction> repository, IInstructionModel model, ILogger<InstructionService> logger)
            : base(repository, model, logger)
        {
        }

        public IEnumerable<IInstruction> Get(string name)
        {
            try
            {
                return Repository.Get(Model.Get(name));
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        public void Update(IInstruction entity, int id)
        {
            try
            {
                Repository.Update(entity, Model.Get(id));
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }
    }
}