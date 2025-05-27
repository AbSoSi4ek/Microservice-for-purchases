using MarketPlaceSale.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;
using MarketPlaceSale.Application.Models.Product;

namespace MarketPlaceSale.Application.Models.Order
{
    public record class CreateOrderModel(
        Guid Id,
        Guid ClientId,
        decimal Money,
        IEnumerable<ProductModel> Products
    ) : ICreateModel;
}
/*
 * 
 * Client client, IEnumerable<Product> products, Money totalAmount
 
public Client Client { get; private set; }

        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.ToList().AsReadOnly();

        public IReadOnlyDictionary<Product, Quantity> ReturnedProducts => _returnedProducts;
        public IReadOnlyDictionary<Seller, ReturnStatus> ReturnStatuses => _returnStatuses;

        public Money TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }


        public OrderDate OrderDate { get; private set; }

        public DeliveryDate DeliveryDate { get; private set; }
 
 */





/*
//если объединить с Line
 
namespace MarketplaceSale.Application.Models.Order
{
    public class OrderLineCreate
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
 
namespace MarketplaceSale.Application.Models.Order
{
    public class OrderCreate
    {
        public Guid ClientId { get; set; }

        public List<OrderLineCreate> Products { get; set; } = [];
    }
}
 */