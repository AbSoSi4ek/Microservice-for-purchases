using MarketplaceSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Exceptions
{
    public class ReturnAlreadyInProgressException(ReturnStatus currentReturnStatus)
        : InvalidOperationException($"Return already in progress or completed (current: {currentReturnStatus}).")
    {
        public ReturnStatus CurrentReturnStatus => currentReturnStatus;
    }
}
