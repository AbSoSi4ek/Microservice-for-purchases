using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Enums;
using MarketplaceSale.Domain.Entities.Base;
using MarketplaceSale.Domain.Exceptions;


namespace MarketplaceSale.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        #region Fields

        private readonly ICollection<OrderLine> _orderLines = [];
        private readonly Dictionary<Product, Quantity> _returnedProducts = [];
        private readonly Dictionary<Seller, ReturnStatus> _returnStatuses = [];

        #endregion

        #region Properties

        public Client Client { get; private set; }

        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.ToList().AsReadOnly();

        public IReadOnlyDictionary<Product, Quantity> ReturnedProducts => _returnedProducts;
        public IReadOnlyDictionary<Seller, ReturnStatus> ReturnStatuses => _returnStatuses;

        public Money TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }


        public OrderDate OrderDate { get; private set; }

        public DeliveryDate DeliveryDate { get; private set; }
        #endregion

        #region Constructors

        public Order(Client client, IEnumerable<Product> products)
            : this(client, products, new Money(products.Sum(p => p.Price.Value))) { }




        protected Order(Client client, IEnumerable<Product> products, Money totalAmount)
            : base(Guid.NewGuid())
        {
            if (client is null)
                throw new ArgumentNullValueException(nameof(client));
            if (products is null || !products.Any())
                throw new EmptyOrderProductListException();
            if (totalAmount is null)
                throw new ArgumentNullValueException(nameof(totalAmount));

            Client = client;
            TotalAmount = totalAmount;
            Status = OrderStatus.Pending;
            OrderDate = new OrderDate(DateTime.UtcNow);

            foreach (var group in products.GroupBy(p => p))
            {
                _orderLines.Add(new OrderLine(group.Key, new Quantity(group.Count())));
            }
        }

        #endregion

        #region Behavior

        public Money CalculateTotal() // стоимость всего заказа
        {
            return new Money(_orderLines.Sum(l => l.Product.Price.Value * l.Quantity.Value));
        }


        public void MarkAsPaid() // заказ оплачен
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOrderStatusChangeException(Status, OrderStatus.Paid);

            foreach (var line in _orderLines)
            {
                var seller = line.Product.Seller;
                seller.ReduceProductStock(line.Product, line.Quantity);
                seller.AddBalance(CalculateTotal());
                seller.AddSaleHistory(this);
            }

            Status = OrderStatus.Paid;
        }

        public void MarkAsShipped() // заказ в доставке
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOrderStatusChangeException(Status, OrderStatus.Shipped);

            Status = OrderStatus.Shipped;
        }

        public void MarkAsDelivered() // заказ доставлен
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOrderStatusChangeException(Status, OrderStatus.Delivered);

            Status = OrderStatus.Delivered;
            DeliveryDate = new DeliveryDate(DateTime.UtcNow); ;
        }

        public void MarkAsCompleted() // заказ выполнен
        {
            if (Status != OrderStatus.Delivered)
                throw new InvalidOrderStatusChangeException(Status, OrderStatus.Delivered);

            Status = OrderStatus.Completed;
        }


        public void Cancel() // заказ отменён
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOrderCancellationException(Status);

            Status = OrderStatus.Cancelled;
        }

        public void RequestReturn(Seller seller) // запрос на возврат
        {
            if (Status != OrderStatus.Completed)
                throw new InvalidReturnRequestException(Status);

            if (_returnStatuses.TryGetValue(seller, out var status) && status != ReturnStatus.None)
                throw new ReturnAlreadyInProgressException(status);

            if (!_orderLines.Any(line => line.Product.Seller == seller))
                throw new SellerNotFoundException(this.Status);

            _returnStatuses[seller] = ReturnStatus.Requested;
        }


        public void RejectReturn(Seller seller) // возврат отклонён
        {
            if (seller is null)
                throw new ArgumentNullValueException(nameof(seller));

            if (!_returnStatuses.TryGetValue(seller, out var status) || status != ReturnStatus.Requested)
                throw new ReturnNotRequestedException();

            _returnStatuses[seller] = ReturnStatus.Rejected;
        }


        public void ApproveReturn(Seller seller) // возврат одобрен
        {
            if (seller is null)
                throw new ArgumentNullValueException(nameof(seller));

            if (!_returnStatuses.TryGetValue(seller, out var status) || status != ReturnStatus.Requested)
                throw new ReturnNotRequestedException();

            _returnStatuses[seller] = ReturnStatus.Approved;
        }


        public void PartialRefund(Seller seller, Product product, Quantity quantity) // частичный возврат
        {
            if (!_returnStatuses.TryGetValue(seller, out var status) || status != ReturnStatus.Approved)
                throw new ReturnNotApprovedException();

            var line = _orderLines.FirstOrDefault(l => l.Product == product)
                ?? throw new ProductNotInOrderException(product);

            if (line.Product.Seller != seller)
                throw new ProductDoesNotBelongToSellerException(product, seller);

            var alreadyReturned = _returnedProducts.ContainsKey(product)
                ? _returnedProducts[product].Value : 0;

            var remainingQuantity = line.Quantity.Value - alreadyReturned;

            if (quantity.Value > remainingQuantity)
                throw new InvalidRefundQuantityException(product, quantity, remainingQuantity);

            product.OrderRefundStock(seller, quantity);

            if (_returnedProducts.ContainsKey(product))
                _returnedProducts[product] = new Quantity(_returnedProducts[product].Value + quantity.Value);
            else
                _returnedProducts.Add(product, quantity);

            // Проверка: все ли товары от этого продавца возвращены
            var allReturned = _orderLines
                .Where(l => l.Product.Seller == seller)
                .All(l =>
                    _returnedProducts.TryGetValue(l.Product, out var returned) &&
                    returned.Value >= l.Quantity.Value
                );

            if (allReturned)
                _returnStatuses[seller] = ReturnStatus.Refunded;
            else
                _returnStatuses[seller] = ReturnStatus.PartialRefunded;
        }


        public void MarkAsRefunded(Seller seller)
        {
            if (!_returnStatuses.TryGetValue(seller, out var status) || status != ReturnStatus.Approved)
                throw new ReturnNotApprovedException();

            foreach (var line in _orderLines.Where(l => l.Product.Seller == seller))
            {
                var returned = _returnedProducts.TryGetValue(line.Product, out var q) ? q.Value : 0;
                var remaining = line.Quantity.Value - returned;

                if (remaining > 0)
                {
                    line.Product.OrderRefundStock(seller, new Quantity(remaining));
                    _returnedProducts[line.Product] = new Quantity(returned + remaining);
                }
            }

            _returnStatuses[seller] = ReturnStatus.Refunded;
        }



        public void MarkAsNoneRefunded(Seller seller)
        {
            if (!_returnStatuses.TryGetValue(seller, out var status) || status != ReturnStatus.Approved)
                throw new ReturnNotApprovedException();

            _returnStatuses[seller] = ReturnStatus.None;
        }

        /*public void UpdateStatus(OrderStatus newStatus) // обновить статус
        {
            Status = OrderStatus.newStatus;
        }
        */
        #endregion
    }
}
//я устал, я иду спать, спокойной ночи