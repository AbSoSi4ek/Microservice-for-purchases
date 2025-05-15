using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
namespace MarketplaceSale.Domain.Exceptions
{
    public class QuantityDecreaseExceedsAvailableException(Product product, Quantity attempted, Quantity available)
        : InvalidOperationException($"Attempted to remove {attempted} units from product {product.ProductName}, but only {available} available.")
    {
        public Product Product => product;
        public Quantity Attempted => attempted;
        public Quantity Available => available;
    }
}
