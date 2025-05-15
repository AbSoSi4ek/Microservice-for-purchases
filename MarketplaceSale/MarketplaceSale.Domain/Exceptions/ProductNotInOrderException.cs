using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductNotInOrderException(Product product)
        : InvalidOperationException($"Product {product.ProductName} is not part of this order.")
    {
        public Product Product => product;
    }
}
