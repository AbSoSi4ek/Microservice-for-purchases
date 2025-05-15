using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class InvalidOrderStatusChangeException (OrderStatus currentStatus, OrderStatus targetStatus)
        : InvalidOperationException($"Cannot change order status from '{currentStatus}' to '{targetStatus}'.")
    {
        public OrderStatus CurrentStatus => currentStatus;
        public OrderStatus TargetStatus => targetStatus;
    }
}
