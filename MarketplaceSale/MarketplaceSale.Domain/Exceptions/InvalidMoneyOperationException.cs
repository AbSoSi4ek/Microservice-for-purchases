using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketplaceSale.Domain.Exceptions
{
    public class InvalidMoneyOperationException (Money amount)
        : InvalidOperationException($"Invalid operation with money amount: {amount}. Amount must be positive and valid.")
    {
        public Money Amount => amount;
    }
}
