using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Enums
{
    public enum ReturnStatus
    {
        None,           //ничего
        Requested,      //запрос на возврат
        Approved,       //одобрен
        Rejected,       //отклонён
        Refunded,       //возврат выполнен
        //PartialRefunded //частичный возврат
    }
}

