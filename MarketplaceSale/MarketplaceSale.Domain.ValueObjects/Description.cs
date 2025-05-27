using MarketplaceSale.Domain.ValueObjects.Validators;
using MarketplaceSale.Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketplaceSale.Domain.ValueObjects
{
    public class Description(string value)
        : ValueObject<string>(new DescriptionValidator(), value);

}

/*
 
public sealed class value : ValueObject<string>
{
    public value(string value)
        : base(new DescriptionValidator(), value) { }

    public int Length => Value.Length;
}
 
 */