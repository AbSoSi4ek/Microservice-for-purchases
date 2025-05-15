 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Validators;
using MarketplaceSale.Domain.ValueObjects.Base;

namespace MarketplaceSale.Domain.ValueObjects
{
    public class FavoriteId_НЕ_ИСПОЛЬЗУЕТСЯ (int favoriteId)
        : ValueObject<int>(new IdValidator_НЕ_ИСПОЛЬЗУЕТСЯ(), favoriteId);
}

