using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    public class UserNameTooLongException(string value, int maxLength)
        : FormatException($"Product name '{value}' is longer than minimum allowed length {maxLength}")
    {
        public string Value => value;
        public int MaxLength => maxLength;
    }
}
