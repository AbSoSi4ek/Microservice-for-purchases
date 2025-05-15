using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Exceptions
{
    public class UnauthorizedOrderAccessException (Client client, Order order)
        : InvalidOperationException ($"Seller '{client.Username}' is not authorized to access order with ID '{order.Id}'.")
    {
        public Client Client => client;
        public Order Order => order;
    }
}
