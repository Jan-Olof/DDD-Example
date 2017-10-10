using System;

namespace Helpers.Functional.Validators
{
    public class MakeTransfer : Command
    {
        public decimal Amount { get; set; }

        public string Beneficiary { get; set; }

        public string Bic { get; set; }

        public DateTime Date { get; set; }

        public Guid DebitedAccountId { get; set; }

        public string Iban { get; set; }
    }
}