﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Validators;

namespace MarketplaceSale.Domain.ValueObjects
{
    public class Quantity(int value)
        : ValueObject<int>(new StockQuantityValidator(), value);
}
