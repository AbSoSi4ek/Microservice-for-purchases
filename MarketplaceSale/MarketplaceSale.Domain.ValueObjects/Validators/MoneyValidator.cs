using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Validators
{
    internal class MoneyValidator : IValidator<decimal>
    {
        public void Validate(decimal value)
        {
            if (value <= 0)
                throw new MoneyNonPositiveException(ExceptionMessages.MONEY_MUST_BE_POSITIVE, nameof(value), value);
            if (decimal.Round(value, 2) != value)
                throw new MoneyHasMoreThanTwoDecimalPlacesException(ExceptionMessages.MONEY_MAX_TWO_DECIMAL_PLACES, nameof(value), value);
        }

    }
}
