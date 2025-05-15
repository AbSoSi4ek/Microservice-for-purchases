using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Exceptions;

namespace MarketplaceSale.Domain.ValueObjects.Validators
{
    internal class OrderDateValidator : IValidator<DateTime>
    {
        public void Validate(DateTime value)
        {
            {
                if (value == default)
                    throw new OrderDateDefaultValueException(ExceptionMessages.DATE_IS_DEFAULT_VALUE, nameof(value), value);

                if (value > DateTime.UtcNow)
                    throw new OrderDateFromFutureException(ExceptionMessages.DATE_IS_FUTURE_VALUE, nameof(value), value);

                if (value.Year < 2000)
                    throw new OrderDateTooOld(ExceptionMessages.DATE_IS_OLD_VALUE, nameof(value), value);
            }
            
        }
    }
}
