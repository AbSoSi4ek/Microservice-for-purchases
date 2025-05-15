using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ArgumentNullValueException(string paramName)
            : ArgumentNullException(paramName, $"Argument \"{paramName}\" value is null");
}
