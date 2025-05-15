using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketplaceSale.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,    // Ожидающий оплаты
        Paid,       // Оплаченный
        Shipped,    // Отправленный
        Delivered,  // Доставленный
        Completed,  // Выполнен
        Cancelled,   // Отменён
        CancelledDueToRefund // Отменён из-за возврата
    }
}
