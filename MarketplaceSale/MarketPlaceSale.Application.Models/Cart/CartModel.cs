using MarketPlaceSale.Application.Models.Base;
using MarketPlaceSale.Application.Models.CartLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Cart
{
    public record class CartModel(
        Guid Id,
        Guid ClientId
        
    ) : IModel<Guid>
    {
        public required IEnumerable<CartLineModel> CartLines { get; init; }
    }
}
