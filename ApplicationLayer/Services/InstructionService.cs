using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Utilities.Enums;
using ApplicationLayer.Interfaces.Infrastructure;

namespace ApplicationLayer.Services
{
    /// <summary>
    /// The instruction service class. Handles the stories/tasks concerning instructions.
    /// </summary>
    public class InstructionService : BaseService<IInstruction, IInstructionModel>, IInstructionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionService"/> class.
        /// </summary>
        public InstructionService(IRepository<IInstruction> repository, IInstructionModel model, ILogger<InstructionService> logger)
            : base(repository, model, logger)
        {
        }

        /// <summary>
        /// Get instruction by name.
        /// </summary>
        public IList<IInstruction> Get(string name)
        {
            try
            {
                return Repository.Get(Model.Get(name)).ToList();
            }
            catch (Exception e)
            {
                Logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Update an instruction.
        /// </summary>
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