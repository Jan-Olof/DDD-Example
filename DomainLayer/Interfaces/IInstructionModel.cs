using System;
using System.Linq.Expressions;

namespace DomainLayer.Interfaces
{
    public interface IInstructionModel : IModel<IInstruction>
    {
        Expression<Func<IInstruction, bool>> Get(string name);
    }
}