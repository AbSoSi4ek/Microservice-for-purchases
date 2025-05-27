using MarketplaceSale.Domain.Entities.Base;
using MarketplaceSale.Domain.Enums;
using MarketplaceSale.Domain.Exceptions;
using MarketplaceSale.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Entities
{
    public class CartLine : Entity<Guid>
    {
        #region Properties

        //public Guid ProductId { get; private set; }
        //public Guid CartId { get; internal set; }
        public Cart Cart { get; private set; }



        public Product Product { get; private set; }
        public Quantity Quantity { get; private set; }

        public CartSelectionStatus SelectionStatus { get; private set; }
        #endregion

        #region Constructor

        private CartLine() { }
        public CartLine(Product product, Quantity quantity) : base(Guid.NewGuid())
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));
            if (quantity is null)
                throw new ArgumentNullValueException(nameof(quantity));

            if (quantity.Value <= 0)
                throw new QuantityMustBePositiveException(product, quantity);

            SelectionStatus = CartSelectionStatus.Unselected;
            Product = product;
            //ProductId = product.Id;
            Quantity = quantity;
        }

        #endregion

        // методы для установки количества уже есть, осталось использовать их в ордере и клиенте

        public void IncreaseQuantity(Quantity amount)
        {
            if (amount.Value <= 0)
                throw new QuantityMustBePositiveException(Product, amount);

            Quantity = new Quantity(Quantity.Value + amount.Value);
        }

        public void DecreaseQuantity(Quantity amount)
        {
            if (amount.Value <= 0)
                throw new QuantityMustBePositiveException(Product, amount);

            if (amount.Value > Quantity.Value)
                throw new QuantityDecreaseExceedsAvailableException(Product, amount, Quantity);

            Quantity = new Quantity(Quantity.Value - amount.Value);
        }

        public void SelectProduct() => SelectionStatus = CartSelectionStatus.Selected;
        public void UnselectProduct() => SelectionStatus = CartSelectionStatus.Unselected;

        public Money GetPrice()
        {
            return Product.Price * Quantity.Value;
        }
        
    }
}
