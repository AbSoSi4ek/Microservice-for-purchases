using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    public class DescriptionTooShortException(string value, int maxLength)
        : FormatException($"Product name '{value}' is longer than maximum allowed length {maxLength}")
    {
        public string Value => value;
        public int MaxLength => maxLength;
    }
}
