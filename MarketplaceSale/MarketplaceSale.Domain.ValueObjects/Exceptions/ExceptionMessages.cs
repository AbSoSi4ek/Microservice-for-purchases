using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketplaceSale.Domain.ValueObjects.Exceptions
{
    internal class ExceptionMessages // спросить как нормально сделать исключения для всего
    {
        public const string VALUE_CANNOT_BE_NULL_OR_WHITE_SPACE = "Value must not be null, empty or whitespace.";
        public const string VALIDATOR_MUST_BE_SPECIFIED = "Validator must be specified for type.";
        public const string MONEY_MUST_BE_POSITIVE = "Money amount must be greater than zero.";
        public const string MONEY_MAX_TWO_DECIMAL_PLACES = "Money amount must not have more than two decimal places.";
        //public const string PRICE_MUST_BE_POSITIVE = "Price amount must be greater than zero.";
        public const string DATE_IS_DEFAULT_VALUE = "Order date cannot be default value.";
        public const string DATE_IS_FUTURE_VALUE = "Order date cannot be in the future.";
        public const string DATE_IS_OLD_VALUE = "Order date is unrealistically old.";
    }
}
