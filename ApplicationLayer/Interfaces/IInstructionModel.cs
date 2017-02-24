using System;
using System.Linq.Expressions;

namespace ApplicationLayer.Interfaces
{
    public interface IInstructionModel : IModel<IInstruction>
    {
        Expression<Func<IInstruction, bool>> Get(string name);
    }
}