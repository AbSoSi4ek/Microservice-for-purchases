using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductWithoutSellerException (Product product)
        : InvalidOperationException($"The product {product.ProductName} was removed from sale")
    {

    }
}
