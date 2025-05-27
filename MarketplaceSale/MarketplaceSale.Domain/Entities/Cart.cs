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

        private readonly ICollection<CartLine> _cartLines = new List<CartLine>();

        #endregion

        #region Properties

        public Client Client { get; private set; }

        //public Guid ClientId { get; private set; }

        public IReadOnlyCollection<CartLine> CartLines => (IReadOnlyCollection<CartLine>)_cartLines;




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

        // потом добавить методы для изменения количества продуктов в картлайне ChangeQuantity(Product product, Quantity quantity) или DecreaseProductQuantity
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


