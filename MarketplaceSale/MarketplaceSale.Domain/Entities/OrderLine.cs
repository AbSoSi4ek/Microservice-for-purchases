using MarketplaceSale.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities.Base;

namespace MarketplaceSale.Domain.Entities
{
    public class OrderLine : Entity<Guid>
    {
        #region Properties

        public Product Product { get; }

        public Quantity Quantity { get; private set; }

        public Seller Seller { get; private set; } // эксперимент для связи в бд
        // эксперимент удачный, но стоило ли это того 🥺


        #endregion

        #region Constructor

        private OrderLine() { }
        public OrderLine(Product product, Quantity quantity) : base(Guid.NewGuid())
        {
            if (product is null)
                throw new ArgumentNullValueException(nameof(product));

            if (quantity is null)
                throw new ArgumentNullValueException(nameof(quantity));

            if (quantity.Value <= 0)
                throw new QuantityMustBePositiveException(product, quantity);

            Product = product;
            Quantity = quantity;
        }
        

        #endregion

        #region Behavior

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

        #endregion
    }
}
