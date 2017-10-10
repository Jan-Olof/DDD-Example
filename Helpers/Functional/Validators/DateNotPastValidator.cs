using System;

namespace Helpers.Functional.Validators
{
    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        private readonly DateTime _today;

        public DateNotPastValidator(DateTime today)
            => _today = today;

        public bool IsValid(MakeTransfer cmd)
            => _today <= cmd.Date.Date;
    }
}