using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductDoesNotBelongToSellerException(Product product, Seller seller)
        : InvalidOperationException($"Seller {seller.Id} does not own product {product.ProductName}.")
    {
        public Product Product => product;
        public Seller Seller => seller;
    }
}
