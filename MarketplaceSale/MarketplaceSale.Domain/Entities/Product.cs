using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities.Base;
using MarketplaceSale.Domain.Exceptions;

namespace MarketplaceSale.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        #region Properties

        public ProductName ProductName { get; private set; }
        public Description Description { get; private set; }
        public Money Price { get; private set; }
        public Quantity StockQuantity { get; private set; }
        public Seller Seller { get; private set; }

        #endregion

        #region Constructors

        protected Product() { }

        public Product(
            ProductName productName,
            Description description,
            Money price,
            Quantity stockQuantity,
            Seller seller)
            : this(Guid.NewGuid(), productName, description, price, stockQuantity, seller) { }

        protected Product(
            Guid id,
            ProductName productName,
            Description description,
            Money price,
            Quantity stockQuantity,
            Seller seller)
            : base(id)
        {
            ProductName = productName ?? throw new ArgumentNullValueException(nameof(productName));
            Description = description ?? throw new ArgumentNullValueException(nameof(description));
            Price = price ?? throw new ArgumentNullValueException(nameof(price));
            StockQuantity = stockQuantity ?? throw new ArgumentNullValueException(nameof(stockQuantity));
            Seller = seller ?? throw new ArgumentNullValueException(nameof(seller));
        }

        #endregion

        #region Behavior

        public void AssignToSeller(Seller seller)
        {
            if (Seller != null && Seller != seller)
                throw new ProductAlreadyAssignedToAnotherSellerException(this, Seller, seller);

            Seller = seller;
        }

        public void UnassignSeller()
        {
            Seller = null!;
        }


        public void SellerIncreaseStock(Seller seller, Quantity additionalQuantity)
        {
            if (additionalQuantity.Value <= 0)
                throw new QuantityMustBePositiveException(this, additionalQuantity);
            if (seller != Seller)
                throw new ProductDoesNotBelongToSellerException(this, seller);
            StockQuantity = new Quantity(StockQuantity.Value + additionalQuantity.Value);
        }

        public void SellerDecreaseStock(Seller seller, Quantity additionalQuantity)
        {
            if (additionalQuantity.Value <= 0)
                throw new QuantityMustBePositiveException(this, additionalQuantity);
            if (additionalQuantity.Value >= StockQuantity.Value)
                throw new QuantityDecreaseExceedsAvailableException(this, additionalQuantity, StockQuantity);
            if (seller != Seller)
                throw new ProductDoesNotBelongToSellerException(this, seller);
            StockQuantity = new Quantity(StockQuantity.Value - additionalQuantity.Value);
        }

        // я думаю можно это убрать, либо добавить статус возврата по причине брака или не подошло
        public void OrderRefundStock(Seller seller, Quantity additionalQuantity) // увеличиваем наличие товара при удачном возврате
        {
            if (additionalQuantity.Value <= 0)
                throw new QuantityMustBePositiveException(this, additionalQuantity);
            if (seller != Seller)
                throw new ProductDoesNotBelongToSellerException(this, seller);
            StockQuantity = new Quantity(StockQuantity.Value + additionalQuantity.Value);

        }

        public void OrderRemoveStock(Seller seller, Quantity additionalQuantity)
        {
            if (additionalQuantity.Value <= 0)
                throw new QuantityMustBePositiveException(this, additionalQuantity);
            if (additionalQuantity.Value >= StockQuantity.Value)
                throw new QuantityDecreaseExceedsAvailableException(this, additionalQuantity, StockQuantity);
            if (seller != Seller)
                throw new ProductDoesNotBelongToSellerException(this, seller);
            StockQuantity = new Quantity(StockQuantity.Value - additionalQuantity.Value);

        }

        // сделать методы
        /*
         SellerUpdateStock (2 метода) - может обновлять наличие товара, но только тот который он продаёт

        OrderUpdateStock (2 метода)- может уменьшать наличие товара при продаже (только то что купили или было в корзине), при возврате наличие добавляет на то количество которое вернули 
         
         */

        public void ChangePrice(Money newPrice, Seller seller)
        {
            if (seller != Seller)
                throw new ProductDoesNotBelongToSellerException(this, seller);

            if (newPrice == null)
                throw new ArgumentNullValueException(nameof(newPrice));

            Price = newPrice;
        }

        #endregion
    }
}
        
