using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Validators
{
    internal class IdValidator_НЕ_ИСПОЛЬЗУЕТСЯ : IValidator<int>
    {
        public void Validate(int value)
        {
            if (value <= 0)
                throw new ArgumentException("ID must be a positive integer.");

        }
    }
}
