using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Validators
{
    internal class DescriptionValidator : IValidator<string>
    {
        public static int MIN_LENGTH => 10;
        public static int MAX_LENGTH => 500;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.VALUE_CANNOT_BE_NULL_OR_WHITE_SPACE);

            if (value.Length < MIN_LENGTH)
                throw new DescriptionTooShortException(value, MIN_LENGTH);

            if (value.Length > MAX_LENGTH)
                throw new DescriptionTooLongException(value, MAX_LENGTH);
        }
    }
}
