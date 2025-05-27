using MarketPlaceSale.Application.Models.Base;
using MarketPlaceSale.Application.Models.Cart;
using MarketPlaceSale.Application.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Client
{
    public record class ClientModel(
        Guid Id,
        string Username,
        decimal AccountBalance,
        Guid CartId
    ) : IModel<Guid>
    {
        public required IReadOnlyCollection<OrderModel> PurchaseHistory { get; init; }

        public required IReadOnlyCollection<OrderModel> ReturnHistory { get; init; }
    }
}
