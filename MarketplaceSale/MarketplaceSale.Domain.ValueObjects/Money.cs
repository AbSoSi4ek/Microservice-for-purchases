
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketplaceSale.Domain.ValueObjects.Validators;
using MarketplaceSale.Domain.ValueObjects.Base;


namespace MarketplaceSale.Domain.ValueObjects

{
    /// <summary>
    /// Represents type of the money.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class Money(decimal amountInRub) : ValueObject<decimal>(
        new MoneyValidator(),
        Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
    {
        public static Money operator +(Money m1, Money m2)
            => new(m1.Value + m2.Value);

        public static Money operator -(Money m1, Money m2)
            => new(m1.Value - m2.Value);

        public static Money operator *(Money money, int multiplier)
            => new(money.Value * multiplier);

        public static Money operator *(int multiplier, Money money)
            => new(money.Value * multiplier);

        public static bool operator >(Money m1, Money m2)
            => m1.Value > m2.Value;

        public static bool operator <(Money m1, Money m2)
            => m1.Value < m2.Value;

        public static bool operator >=(Money m1, Money m2)
            => m1.Value >= m2.Value;

        public static bool operator <=(Money m1, Money m2)
            => m1.Value <= m2.Value;


    }
}

/*
{
public class Money(decimal amount)
    : ValueObject<decimal>(new MoneyValidator(), amount)
{
    public decimal Amount => Value;

    public override string ToString() => $"{Amount}";
}
}*/

/*
 
 public sealed class Money : ValueObject<decimal>
{
    public Money(decimal amount, Currency currency)
        : base(new MoneyValidator(), amount)
    {
....
    }
}
 
 */