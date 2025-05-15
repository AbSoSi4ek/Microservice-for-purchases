using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductNotFoundInCartException(Product product)
        : InvalidOperationException($"Product with ID '{product.Id}' was not found in the cart.")
    {
        public Product Product => product;
    }
}
