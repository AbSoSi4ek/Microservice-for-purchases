using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductNotInCartException(Product product)
        : InvalidOperationException($"Product {product.ProductName} is not part of cart.")
    {
        public Product Product => product;
    }
}
