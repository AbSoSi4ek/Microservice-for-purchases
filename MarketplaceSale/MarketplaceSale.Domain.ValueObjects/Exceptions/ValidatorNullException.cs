using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    //internal class ValidatorNullException(string paramName, string message) : ArgumentNullException(paramName, message); // Доработать + спросить

    
    public class ValidatorNullException : ArgumentNullException
    {
        public ValidatorNullException(string paramName, string message)
            : base(paramName, message)
        {
        }
    }
    
}
