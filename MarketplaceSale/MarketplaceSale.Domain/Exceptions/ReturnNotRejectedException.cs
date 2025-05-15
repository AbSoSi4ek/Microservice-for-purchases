using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ReturnNotRejectedException (Order order)
        : InvalidOperationException($"Return request for order with ID '{order.Id}' cannot be rejected because it is in an invalid state.")
    {
        public Order Order => order;
    }
}
