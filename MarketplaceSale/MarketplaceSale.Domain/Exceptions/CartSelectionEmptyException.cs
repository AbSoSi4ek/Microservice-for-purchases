using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class CartSelectionEmptyException()
        : InvalidOperationException("The cart selection is empty. Please add products to the cart.")
    {
    }
}
