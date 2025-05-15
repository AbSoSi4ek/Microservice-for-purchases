using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    internal class ArgumentNullOrWhiteSpaceException(string paramName, string message) 
        : ArgumentNullException(paramName, message); // Доработать + спросить

}
