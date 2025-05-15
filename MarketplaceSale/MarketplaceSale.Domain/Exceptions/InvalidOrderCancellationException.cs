using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class InvalidOrderCancellationException(OrderStatus currentStatus)
        : InvalidOperationException($"Cannot cancel order with status {currentStatus}. Only 'Paid' orders can be cancelled.")
    {
        public OrderStatus CurrentStatus => currentStatus;
    }
}
