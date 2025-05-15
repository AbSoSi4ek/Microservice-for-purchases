using MarketplaceSale.Domain.ValueObjects.Validators;
using MarketplaceSale.Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketplaceSale.Domain.ValueObjects
{
    public class Description(string description)
        : ValueObject<string>(new DescriptionValidator(), description);

}

/*
 
public sealed class Description : ValueObject<string>
{
    public Description(string value)
        : base(new DescriptionValidator(), value) { }

    public int Length => Value.Length;
}
 
 */