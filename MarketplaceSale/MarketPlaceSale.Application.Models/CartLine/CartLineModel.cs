using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Enums;
using MarketPlaceSale.Application.Models.Base;

namespace MarketPlaceSale.Application.Models.CartLine
{
    public record class CartLineModel(
        Guid Id,
        Guid ProductId,
        int Quantity,
        CartSelectionStatus SelectionStatus
    ) : IModel<Guid>;
}
