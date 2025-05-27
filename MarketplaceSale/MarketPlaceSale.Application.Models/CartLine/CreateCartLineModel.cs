using MarketPlaceSale.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketPlaceSale.Application.Models.CartLine
{
    public record class CreateCartLineModel(
        Guid Id,
        Guid ProductId,
        int Quantity
    ) : ICreateModel;
}
