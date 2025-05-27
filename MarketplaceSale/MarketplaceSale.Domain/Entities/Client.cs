using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities.Base;
using MarketplaceSale.Domain.Exceptions;
using MarketplaceSale.Domain.Enums;


namespace MarketplaceSale.Domain.Entities
{
    public class Client(Guid id, Username username) : Entity<Guid>(id)
    {
        #region Fields

        private readonly List<Order> _purchaseHistory = new List<Order>();
        private readonly List<Order> _returnHistory = new List<Order>();

        #endregion // Fields

        #region Properties

        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        public Money AccountBalance { get; private set; } = new(0);

        public Cart Cart { get; private set; } = null!;


        public IReadOnlyCollection<Order> PurchaseHistory => _purchaseHistory.AsReadOnly();
        public IReadOnlyCollection<Order> ReturnHistory => _returnHistory.AsReadOnly();

        /*public IReadOnlyCollection<Order> PurchaseHistory => _purchaseHistory.ToList().AsReadOnly();

        public IReadOnlyCollection<Order> ReturnHistory => _returnHistory.ToList().AsReadOnly();*/

        #endregion // Properties

        #region Behavior

        internal bool ChangeUsername(Username newUsername) // Смена имени пользователя
        {
            if (Username == newUsername) return false;
            Username = newUsername;
            return true;
        }

        public Money CheckBalance() => AccountBalance; // Проверка баланса

        public void AddToCart(Product product, Quantity quantity) // Добавить в корзину
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));
            if (quantity is null || quantity.Value <= 0)
                throw new QuantityMustBePositiveException(product, quantity);

            Cart.AddProduct(product, quantity);
        }

        public void RemoveFromCart(Product product) // Убрать из корзины
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));
            bool productExistsInCart = Cart.CartLines.Any(cl => cl.Product == product);
            if (!productExistsInCart)
                throw new ProductNotFoundInCartException(product);
            Cart.RemoveProduct(product);
        }



        public void ClearCart() => Cart.ClearCart(); // Очистить корзину

        public void AddBalance(Money amount) // Увеличить баланс
        {
            if (amount is null || amount.Value <= 0)
                throw new InvalidMoneyOperationException(amount);

            AccountBalance += amount;
        }

        public void SubtractBalance(Money amount) // Уменьшить баланс
        {
            if (amount is null || amount.Value <= 0)
                throw new InvalidMoneyOperationException(amount);

            if (amount > AccountBalance)
                throw new NotEnoughFundsException(AccountBalance, amount);

            AccountBalance -= amount;
        }

        #endregion // Behavior

        #region Ordering

        public void SelectProductForOrder(Product product) // выбрать продукт в корзине для заказа
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));

            var line = Cart.CartLines.FirstOrDefault(p => p.Product == product);
            if (line is null)
                throw new ProductNotFoundInCartException(product); // нужно создать

            line.SelectProduct();
        }

        public void UnselectProductForOrder(Product product) // антивыбрать продукт в корзине для заказа
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));

            var line = Cart.CartLines.FirstOrDefault(p => p.Product == product);
            if (line is null)
                throw new ProductNotFoundInCartException(product); // нужно создать

            line.UnselectProduct();
        }


        public Order PlaceSelectedOrderFromCart() // сделать заказ из выбранных в корзине товаров
        {

            var selectedLines = Cart.CartLines
                .Where(line => line.SelectionStatus == CartSelectionStatus.Selected)
                .ToList();

            if (!selectedLines.Any())
                throw new CartSelectionEmptyException(); // нужно создать исключение

            foreach (var line in selectedLines)
            {
                if (line.Quantity is null || line.Quantity.Value <= 0)
                    throw new QuantityMustBePositiveException(line.Product, line.Quantity);
                if (line.Product.StockQuantity.Value < line.Quantity.Value)
                    throw new NotEnoughStockException(line.Product, line.Quantity);
            }

            var selectedProducts = selectedLines
                .SelectMany(line => Enumerable.Repeat(line.Product, line.Quantity.Value))
                .ToList();

            var total = selectedProducts.Sum(p => p.Price.Value);
            var totalMoney = new Money(total);

            if (totalMoney > AccountBalance)
                throw new NotEnoughFundsException(AccountBalance, totalMoney);

            var order = new Order(this, selectedProducts);
            foreach (var group in selectedProducts.GroupBy(p => p))
            {
                var quantity = new Quantity(group.Count());
                group.Key.OrderRemoveStock(group.Key.Seller, quantity);
            }

            _purchaseHistory.Add(order);
            Cart.ClearSelected();
            return order;
        }



        public Order PlaceDirectOrder(Product product, Quantity quantity) // сделать заказ сразу со страницы товара
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));
            if (quantity is null || quantity.Value <= 0)
                throw new QuantityMustBePositiveException(product, quantity);
            if (product.StockQuantity.Value < quantity.Value)
                throw new NotEnoughStockException(product, quantity);

            var products = Enumerable.Repeat(product, quantity.Value).ToList();
            var order = new Order(this, products); // статус будет Pending

            _purchaseHistory.Add(order);

            return order;
        }


        public void PayForOrder(Order order)
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (order.Client != this)
                throw new UnauthorizedOrderAccessException(this, order);
            if (order.Status != OrderStatus.Pending)
                throw new InvalidOrderStatusChangeException(order.Status, OrderStatus.Paid);

            var total = order.CalculateTotal();

            if (total > AccountBalance)
                throw new NotEnoughFundsException(AccountBalance, total);

            SubtractBalance(total);

            foreach (var line in order.OrderLines)
            {
                line.Product.OrderRemoveStock(line.Product.Seller, line.Quantity);
                line.Product.Seller.AddBalance(new Money(line.Product.Price.Value * line.Quantity.Value));
                line.Product.Seller.AddSaleHistory(order);
            }

            order.MarkAsPaid();
        }


        public void CancelOrder(Order order) // Отменить заказ
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (order.Client != this)
                throw new UnauthorizedOrderAccessException(this, order);
            if (order.Status != OrderStatus.Paid)
                throw new InvalidOrderCancellationException(order.Status);

            order.Cancel();
            AddBalance(order.TotalAmount);
        }

        public void RequestReturnOrder(Order order, Seller seller) // запрос на возврат товара
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (seller is null)
                throw new ArgumentNullValueException(nameof(seller));
            if (order.Client != this)
                throw new UnauthorizedOrderAccessException(this, order);
            if (order.Status != OrderStatus.Completed)
                throw new InvalidReturnRequestException(order.Status);

            order.RequestReturn(seller);

            if (!_returnHistory.Contains(order))
                _returnHistory.Add(order);
        }


        public void FinalizeApprovedReturn(Order order, Seller seller) // возврат товара
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (seller is null)
                throw new ArgumentNullValueException(nameof(seller));
            if (order.Client != this)
                throw new UnauthorizedOrderAccessException(this, order);

            order.MarkAsRefunded(seller);

            // Возврат средств за товары этого продавца
            var refundAmount = order.OrderLines
                .Where(line => line.Product.Seller == seller)
                .Sum(line => line.Product.Price.Value * line.Quantity.Value);

            AddBalance(new Money(refundAmount));
        }


        public void RemoveRejectedReturn(Order order, Seller seller) // возврат отклонён продавцом
        {
            if (order is null)
                throw new ArgumentNullValueException(nameof(order));
            if (seller is null)
                throw new ArgumentNullValueException(nameof(seller));
            if (order.Client != this)
                throw new UnauthorizedOrderAccessException(this, order);

            var status = order.ReturnStatuses.TryGetValue(seller, out var value) ? value : ReturnStatus.None;
            if (status != ReturnStatus.Rejected)
                throw new ReturnNotRejectedException(order);

            order.MarkAsNoneRefunded(seller);
        }



        #endregion // Ordering
    }
    
}