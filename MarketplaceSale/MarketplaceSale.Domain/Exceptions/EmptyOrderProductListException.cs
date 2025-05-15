using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class EmptyOrderProductListException()
        : ArgumentException("Order must contain at least one product.")
    { }
}
