using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class SellerNotFoundException(OrderStatus orderStatus) : InvalidOperationException($"No seller found for the order with status '{orderStatus}'.")
    {
        public OrderStatus OrderStatus => orderStatus;
    }
}
