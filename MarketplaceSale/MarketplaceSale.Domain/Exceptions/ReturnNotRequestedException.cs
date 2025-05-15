using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ReturnNotRequestedException()
        : InvalidOperationException("Cannot approve return: no return was requested.")
    { }
}
