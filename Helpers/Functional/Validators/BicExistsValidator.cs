using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers.Functional.Validators
{
    public sealed class BicExistsValidator : IValidator<MakeTransfer>
    {
        private readonly Func<IEnumerable<string>> _getValidCodes;

        public BicExistsValidator(Func<IEnumerable<string>> getValidCodes)
            => _getValidCodes = getValidCodes;

        public bool IsValid(MakeTransfer cmd)
            => _getValidCodes().Contains(cmd.Bic);
    }
}