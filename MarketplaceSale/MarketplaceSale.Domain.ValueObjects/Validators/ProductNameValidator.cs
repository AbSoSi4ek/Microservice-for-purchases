using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Exceptions;

namespace MarketplaceSale.Domain.ValueObjects.Validators
{
    public sealed class ProductNameValidator : IValidator<string>
    {
        public static int MIN_LENGTH => 3;
        public static int MAX_LENGTH => 100;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.VALUE_CANNOT_BE_NULL_OR_WHITE_SPACE);

            if (value.Length < MIN_LENGTH)
                throw new ProductNameTooShortException(value, MIN_LENGTH);

            if (value.Length > MAX_LENGTH)
                throw new ProductNameTooLongException(value, MAX_LENGTH);
        }
    }
}