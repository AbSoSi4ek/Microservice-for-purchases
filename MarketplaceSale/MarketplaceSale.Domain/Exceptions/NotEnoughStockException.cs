using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketplaceSale.Domain.Exceptions
{
    public class NotEnoughStockException (Product product, Quantity quantity)
        : InvalidOperationException($"Not enough stock for product '{product.ProductName}' (ID: {product.Id}). Requested quantity: {quantity}.")
    {
        public Product Product => product;
        public Quantity Quantity => quantity;
    }
}
