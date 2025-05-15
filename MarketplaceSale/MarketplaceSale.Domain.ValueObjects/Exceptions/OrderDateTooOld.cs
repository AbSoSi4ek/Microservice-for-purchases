using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    internal class OrderDateTooOld(string message, string paramName, DateTime value)
        : ArgumentException(message, paramName)
    {
        public DateTime Value => value;
    }
}
