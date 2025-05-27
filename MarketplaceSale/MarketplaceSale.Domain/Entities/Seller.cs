using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities.Base;
using MarketplaceSale.Domain.Enums;
using MarketplaceSale.Domain.Exceptions;
using MarketplaceSale.Domain.ValueObjects;


namespace MarketplaceSale.Domain.Entities
{
    public class Seller(Guid id, Username username) : Entity<Guid>(id)
    {
        #region Fields

        private readonly ICollection<Product> _products = new List<Product>();
        private readonly ICollection<Order> _salesHistory = new List<Order>();

        #endregion // Fields

        #region Properties

        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        public Money BusinessBalance { get; private set; } = new(0);

        public IReadOnlyCollection<Product> AvailableProducts =>
            _products.Where(p => p.StockQuantity.Value > 0).ToList().AsReadOnly();

        public IReadOnlyCollection<Order> SalesHistory => _salesHistory.ToList().AsReadOnly();

        #endregion // Properties

        #region Behavior

        internal bool ChangeUsername(Username newUsername) // Изменить имя пользователя
        {
            if (Username == newUsername) return false;
            Username = newUsername;
            return true;
        }

        public void AddProduct(Product product) // добавить новый продукт
        {
            if (product == null)
                throw new ArgumentNullValueException(nameof(product));

            if (product.Seller != this)
                product.AssignToSeller(this); // Присвоить товар этому продавцу

            if (_products.Contains(product))
                throw new ProductAlreadyExistsException(this, product);

            _products.Add(product);
        }

        public void RemoveProduct(Product product) // убрать продукт
        {
            if (product == null)
                throw new ArgumentNullValueException(nameof(product));

            if (product.Seller != this)
                throw new ProductDoesNotBelongToSellerException(product, this);

            product.UnassignSeller(); // Отвязываем от продавца
            _products.Remove(product);
        }


        public void ReplenishProduct(Product product, Quantity quantity) //Пополнить наличие продукта
        {
            EnsureOwnership(product);
            product.SellerIncreaseStock(this, quantity);
        }

        public void ReduceProductStock(Product product, Quantity quantity) //Уменьшить наличие продуктов
        {
            EnsureOwnership(product);
            product.SellerDecreaseStock(this, quantity);
        }

        public void ChangeProductPrice(Product product, Price newPrice) // Изменить стоимость продукта
        {
            EnsureOwnership(product);
            product.ChangePrice(newPrice, this);
        }


        public void RejectOrderReturn(Order order) // отклонить возврат
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (!SalesHistory.Contains(order))
                throw new OrderDoesNotBelongToSellerException(order, this);

            order.RejectReturn(this);
            order.Client.RemoveRejectedReturn(order, this); // удаляем из истории возвратов
        }


        public void ApproveOrderReturn(Order order) // одобрить возврат
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (!SalesHistory.Contains(order))
                throw new OrderDoesNotBelongToSellerException(order, this);

            order.ApproveReturn(this);

            // Финализация возврата — возврат на баланс клиента
            var refundAmount = order.OrderLines
                .Where(line => line.Product.Seller == this)
                .Sum(line => line.Product.Price.Value * line.Quantity.Value);

            order.Client.AddBalance(new Money(refundAmount));
            order.MarkAsRefunded(this);
        }


        public void ProcessPartialRefund(Order order, Product product, Quantity quantity) // частичный возврат
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));

            if (product.Seller != this)
                throw new ProductDoesNotBelongToSellerException(product, this);
            if (!order.OrderLines.Any(l => l.Product == product))
                throw new ProductNotInOrderException(product);
            if (!SalesHistory.Contains(order))
                throw new OrderDoesNotBelongToSellerException(order, this);

            order.PartialRefund(this, product, quantity);

            var refundAmount = product.Price * quantity.Value;
            SubtractBalance(refundAmount);
        }

        public void ProcessFullRefund(Order order) // полный возврат
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));

            if (!SalesHistory.Contains(order))
                throw new OrderDoesNotBelongToSellerException(order, this);

            order.MarkAsRefunded(this);
        }


        private void EnsureOwnership(Product product) // проверка владения продукта
        {
            if (product.Seller != this || !_products.Contains(product))
                throw new ProductDoesNotBelongToSellerException(product, this);
        }

        public void AddSaleHistory(Order order)  // Добавить заказ в историю продажи, вызывается в Order
        {
            if (!_salesHistory.Contains(order))
                _salesHistory.Add(order);
        }

        public void AddBalance(Money amount) // Пополнение баланса при продаже, вызывается в Order
        {
            if (amount is null)
                throw new ArgumentNullValueException(nameof(amount));

            BusinessBalance += amount;
        }

        public void SubtractBalance(Money amount) // Уменьшение баланса при возврате товара, вызывается в Order
        {
            if (amount is null)
                throw new ArgumentNullValueException(nameof(amount));

            if (amount > BusinessBalance)
                throw new NotEnoughFundsException(BusinessBalance, amount);

            BusinessBalance -= amount;
        }



        #endregion
    }
}

