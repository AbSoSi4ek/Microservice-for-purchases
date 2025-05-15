using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    public sealed class ProductNameTooShortException(string value, int minLength)
        : FormatException($"Product name '{value}' is shorter than minimum allowed length {minLength}")
    {
        public string Value => value;
        public int MinLength => minLength;
    }
}