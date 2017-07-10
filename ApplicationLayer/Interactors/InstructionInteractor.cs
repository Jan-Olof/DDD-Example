using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using Utilities.Enums;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// The instruction interactor class. Handles the stories/tasks concerning instructions.
    /// </summary>
    public class InstructionInteractor : BaseInteractor<Instruction, IInstruction>, IInstructionInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionInteractor"/> class.
        /// </summary>
        public InstructionInteractor(IRepository<Instruction> repository, IInstruction model, ILogger<InstructionInteractor> logger)
            : base(repository, model, logger)
        {
        }

        /// <summary>
        /// Get instruction by name.
        /// </summary>
        public IList<Instruction> Get(string name)
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
        public void Update(Instruction entity, int id)
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