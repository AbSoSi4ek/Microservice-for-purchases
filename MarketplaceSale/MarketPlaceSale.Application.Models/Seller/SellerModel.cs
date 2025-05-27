using MarketplaceSale.Domain.Entities;
using MarketPlaceSale.Application.Models.Base;
using MarketPlaceSale.Application.Models.Order;
using MarketPlaceSale.Application.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Seller
{
    public record class SellerModel(
        Guid Id,
        string Username,
        decimal BusinessBalance
        ) : IModel<Guid>
    {
        public IReadOnlyCollection<ProductModel> AvailableProducts { get; init; }         

        public IReadOnlyCollection<ProductModel> SalesHistory { get; init; }
    }

}
