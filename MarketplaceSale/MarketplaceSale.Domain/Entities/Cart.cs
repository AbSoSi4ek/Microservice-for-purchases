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

    public class Cart : Entity<Guid>
    {
        #region Fields

        private readonly ICollection<CartLine> _cartLines = [];

        #endregion

        #region Properties

        public Client Client { get; private set; }

        public IReadOnlyCollection<CartLine> CartLines => _cartLines.ToList().AsReadOnly();

        #endregion

        #region Constructors

        protected Cart() { }

        public Cart(Client client)
            : base(Guid.NewGuid())
        {
            Client = client ?? throw new ArgumentNullValueException(nameof(client));
        }

        #endregion

        #region Behavior

        public void AddProduct(Product product, Quantity quantity)
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));
            if (quantity is null || quantity.Value <= 0)
                throw new QuantityMustBePositiveException(product, quantity);

            var line = _cartLines.FirstOrDefault(l => l.Product == product);
            if (line != null)
            {
                line.IncreaseQuantity(quantity);
            }
            else
            {
                _cartLines.Add(new CartLine(product, quantity));
            }
        }

        public void RemoveProduct(Product product)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product == product);
            if (line == null)
                throw new ProductNotInCartException(product);

            _cartLines.Remove(line);
        }

        
        public void SelectAllForBuy() // Выбрать все продукты для заказа
        {
            foreach (var line in _cartLines)
                line.SelectProduct();
        }

        public void UnselectAllForBuy() // Убрать все продукты для заказа
        {
            foreach (var line in _cartLines)
                line.UnselectProduct();
        }

        public void SelectForBuy(Product product) // Выбрать определённый продукт для заказа
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            var line = _cartLines.FirstOrDefault(l => l.Product == product);
            if (line is null)
                throw new InvalidOperationException("Product not found in cart.");

            line.SelectProduct();
        }

        public void UnselectForBuy(Product product) // Убрать определённый продукт из заказа
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            var line = _cartLines.FirstOrDefault(l => l.Product == product);
            if (line is null)
                throw new InvalidOperationException("Product not found in cart.");

            line.UnselectProduct();
        }

        public void ClearSelected()
        {
            var selectedLines = _cartLines.Where(line => line.SelectionStatus == CartSelectionStatus.Selected).ToList();

            foreach (var line in selectedLines)
                _cartLines.Remove(line);
        }


        public void ClearCart()
        {
            _cartLines.Clear();
        }

        public Money GetTotalPrice()
        {
            if (!_cartLines.Any())
                return new Money(0);

            decimal total = 0;
            foreach (var line in _cartLines)
            {
                total += line.Product.Price.Value * line.Quantity.Value;
            }

            return new Money(total);
        }

        #endregion
    }
}



















    /*

    public class Cart : Entity<Guid>
    {
        #region Fields

        private readonly List<Product> _products = [];

        #endregion

        #region Properties

        public Client Client { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        #endregion

        #region Constructors

        protected Cart() { }

        public Cart(Client client)
            : base(Guid.NewGuid())
        {
            Client = client ?? throw new ArgumentNullValueException(nameof(client));
        }

        #endregion

        #region Behavior

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullValueException(nameof(product));

            if (!_products.Contains(product))
                _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            if (!_products.Remove(product))
                throw new InvalidOperationException("Product not found in cart.");
        }

        public IReadOnlyCollection<Product> GetCartProducts() { return _products; }

        public void ClearCart()
        {
            _products.Clear();
        }

        public Money GetTotalPrice()
        {
            if (_products.Count == 0)
                return new Money(0, Currency.RUB);

            var currency = _products[0].Price.Currency;

            decimal total = 0;
            foreach (var product in _products)
            {
                if (product.Price.Currency != currency)
                    throw new InvalidOperationException("All products in cart must have the same currency.");

                total += product.Price.Amount;
            }

            return new Money(total, currency);
        }

        #endregion
    }
    */