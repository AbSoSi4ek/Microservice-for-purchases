using MarketPlaceSale.Application.Models.Base;
using MarketPlaceSale.Application.Models.CartLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Cart
{
    public record class CreateCartModel(
        Guid ClientId,
        Guid Id        
    ) : ICreateModel;
}
