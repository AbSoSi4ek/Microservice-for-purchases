using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ProductAlreadyAssignedToAnotherSellerException(Product product, Seller currentSeller, Seller attemptedSeller)
        : InvalidOperationException($"Product {product.ProductName} is already assigned to seller {currentSeller.Username} and cannot be reassigned to {attemptedSeller.Username}.")
    {
        public Product Product => product;
        public Seller CurrentSeller => currentSeller;
        public Seller AttemptedSeller => attemptedSeller;
    }
}
