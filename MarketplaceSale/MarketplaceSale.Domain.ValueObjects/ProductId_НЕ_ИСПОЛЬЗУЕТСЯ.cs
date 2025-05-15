using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Base;
using MarketplaceSale.Domain.ValueObjects.Validators;

namespace MarketplaceSale.Domain.ValueObjects
{
    public class ProductId_НЕ_ИСПОЛЬЗУЕТСЯ(int productId)
        : ValueObject<int>(new IdValidator_НЕ_ИСПОЛЬЗУЕТСЯ(), productId);
}
