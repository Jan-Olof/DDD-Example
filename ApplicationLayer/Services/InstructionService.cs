using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;

namespace ApplicationLayer.Services
{
    public class InstructionService<T>
    {
        private readonly IRepository<T> _repository;

        public InstructionService(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        public IList<T> GetInstructions()
        {
            return _repository.Get().ToList();
        }
    }
}