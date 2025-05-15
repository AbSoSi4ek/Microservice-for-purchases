using MarketplaceSale.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class NotEnoughFundsException(Money balance, Money attempt)
    : InvalidOperationException($"Not enough funds. Balance: {balance}, Attempted: {attempt}.")
    {
        public Money Balance => balance;
        public Money Attempt => attempt;
    }

}
