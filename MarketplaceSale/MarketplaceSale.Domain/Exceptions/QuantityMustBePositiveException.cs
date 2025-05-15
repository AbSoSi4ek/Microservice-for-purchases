using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketplaceSale.Domain.Exceptions
{
    public class QuantityMustBePositiveException(Product product, Quantity quantity)
        : ArgumentException($"Quantity must be positive. Given: {quantity.Value} for product {product.ProductName}.")
    {
        public Product Product => product;
        public Quantity Quantity => quantity;
    }
}
