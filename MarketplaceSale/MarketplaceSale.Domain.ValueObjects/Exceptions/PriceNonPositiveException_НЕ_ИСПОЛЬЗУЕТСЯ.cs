using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    internal class PriceNonPositiveException_НЕ_ИСПОЛЬЗУЕТСЯ(string message, string paramName, decimal value)
    : ArgumentException(message, paramName)
    {
        public decimal Value => value;
    }
}
