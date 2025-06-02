using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class InvalidReturnRequestException(OrderStatus currentStatus)
        : InvalidOperationException($"Cannot request return: order must be 'Completed, but current status is '{currentStatus}'.")
    {
        public OrderStatus CurrentStatus => currentStatus;
    }
}
