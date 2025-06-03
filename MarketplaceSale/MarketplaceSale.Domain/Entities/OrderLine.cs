using MarketplaceSale.Domain.Exceptions;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities.Base;

namespace MarketplaceSale.Domain.Entities
{
    /// <summary>
    /// Представляет одну строку заказа, содержащую информацию о товаре, его количестве и продавце.
    /// </summary>
    public class OrderLine : Entity<Guid>
    {
        #region Properties

        /// <summary>
        /// Ссылка на заказ, к которому относится данная строка.
        /// </summary>
        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        /// <summary>
        /// Продукт, указанный в строке заказа.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public Quantity Quantity { get; private set; }

        /// <summary>
        /// Продавец, предоставивший указанный товар.
        /// </summary>
        public Seller Seller { get; private set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Приватный конструктор для EF.
        /// </summary>
        private OrderLine() { }
        /// <summary>
        /// Создаёт новый экземпляр строки заказа с указанным продуктом и количеством.
        /// </summary>
        /// <param name="product">Продукт, добавляемый в заказ.</param>
        /// <param name="quantity">Количество единиц товара.</param>
        /// <exception cref="ArgumentNullValueException">Выбрасывается, если продукт или количество не указаны.</exception>
        /// <exception cref="QuantityMustBePositiveException">Выбрасывается, если количество товара не положительно.</exception>
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
