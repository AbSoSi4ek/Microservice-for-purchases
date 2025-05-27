using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.Enums;
using MarketplaceSale.Domain.ValueObjects;
using MarketPlaceSale.Application.Models.Base;
using MarketPlaceSale.Application.Models.OrderLine;
using MarketPlaceSale.Application.Models.Product;
using MarketPlaceSale.Application.Models.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Order
{
    public record class OrderModel(
    Guid Id,
    Guid ClientId

    ) : IModel<Guid>
    {
        public required IEnumerable<OrderLineModel> OrderLines { get; init; }

        public required IEnumerable<ProductModel> ReturnedProducts { get; init; }
        public required IEnumerable<SellerModel> ReturnStatuses { get; init; }

    }

    /*
    public record class OrderModel
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public List<OrderLineModel> OrderLines { get; set; } = [];

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public Dictionary<Guid, int> ReturnedProducts { get; set; } = [];

        public Dictionary<Guid, string> ReturnStatuses { get; set; } = [];
    }*/
}
