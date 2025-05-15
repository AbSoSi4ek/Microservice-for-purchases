using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects
{
    public class OrderDate(DateTime orderDate)
         : ValueObject<DateTime>(new OrderDateValidator(), orderDate);
}
