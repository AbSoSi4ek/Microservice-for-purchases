using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketplaceSale.Domain.ValueObjects
{
    public sealed class Price(decimal amount)
       : Money(amount);
}


/*
 
 public class Price(decimal amount, Currency currency)
        : ValueObject<decimal>(new MoneyValidator(), amount); //Классический 
 
 */


/*
 
 public sealed class Price : Money
{
    public Price(decimal amount, Currency currency) //то же самое что и сейчас но в другой форме
        : base(amount, currency)
    { }
}
 
 */


/*
 //Способ без наследования. Money используется внутри класса
 public sealed class Price
{
    public Money Value { get; }

    public Price(decimal amount, Currency currency)
    {
        Value = new Money(amount, currency);
    }

    public override string ToString() => Value.ToString();
}
 
 */
