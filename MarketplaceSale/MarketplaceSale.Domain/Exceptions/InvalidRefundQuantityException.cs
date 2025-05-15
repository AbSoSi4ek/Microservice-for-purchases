using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class InvalidRefundQuantityException(Product product, Quantity requested, int availableToRefund)
        : InvalidOperationException($"Cannot refund {requested.Value} units of {product.ProductName}. Only {availableToRefund} available to refund.")
    {
        public Product Product => product;
        public Quantity Requested => requested;
        public int AvailableToRefund => availableToRefund;
    }
}
