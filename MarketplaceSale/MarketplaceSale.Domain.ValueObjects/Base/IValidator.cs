using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Base 
{
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates data
        /// </summary>
        /// <param name="value"></param>
        void Validate(T value);
    }
}
