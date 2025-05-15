using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductAlreadyExistsException(Seller seller, Product product)
    : InvalidOperationException($"Product {product.ProductName} already exists in seller {seller.Username}'s catalog.")
    {
        public Seller Seller => seller;
        public Product Product => product;
    }

}
