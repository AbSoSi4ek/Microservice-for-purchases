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
    public class Favorite_КРИНЖ_УБРАТЬ : Entity<Guid>
    {
        #region Fields

        private readonly List<Product> _products = [];

        #endregion

        #region Properties

        public Client Client { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        #endregion

        #region Constructors

        protected Favorite_КРИНЖ_УБРАТЬ() { }

        public Favorite_КРИНЖ_УБРАТЬ(Client client)
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
            _products.Remove(product); // без ошибки, если товара нет — просто ничего не делает
        }

        public void ClearFavorites()
        {
            _products.Clear();
        }

        public IReadOnlyCollection<Product> GetFavoriteProducts()
        {
            return Products;
        }

        #endregion
    }
}
