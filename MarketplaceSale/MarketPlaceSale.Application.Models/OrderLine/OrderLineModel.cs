using MarketPlaceSale.Application.Models.Base;
using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.OrderLine
{
    public record class OrderLineModel(
        Guid Id,
        Guid ProductId,
        int Quantity        
    ) : IModel<Guid>;
}
