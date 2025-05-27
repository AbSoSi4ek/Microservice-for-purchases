using MarketPlaceSale.Application.Models.Order;
using MarketPlaceSale.Application.Models.OrderLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface IOrderApplicationService
    {
        Task<OrderModel?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);

        /*Task<OrderModel?> GetOrderByUsernameAsync(string username, CancellationToken cancellationToken);*/

        Task<IEnumerable<OrderModel>> GetOrderAsync(CancellationToken cancellationToken);

        Task<OrderModel?> CreateOrderAsync(CreateOrderModel OrderInformation, CancellationToken cancellationToken);

        Task<bool> UpdateOrderAsync(OrderModel Order, CancellationToken cancellationToken);

        Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken);

        /*
        // Получить заказ по его ID
        Task<OrderModel?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);

        // Получить все заказы клиента
        Task<IReadOnlyCollection<OrderModel>> GetOrdersByClientAsync(Guid clientId, CancellationToken cancellationToken);

        // Создать заказ (на основе списка OrderLineModel)
        Task<Guid> CreateOrderAsync(Guid clientId, IEnumerable<OrderLineModel> orderLines, CancellationToken cancellationToken);

        // Оплатить заказ
        Task<bool> PayOrderAsync(Guid orderId, CancellationToken cancellationToken);

        // Отменить заказ
        Task<bool> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken);

        // Пометить заказ как отправленный
        Task<bool> MarkAsShippedAsync(Guid orderId, CancellationToken cancellationToken);

        // Пометить заказ как доставленный
        Task<bool> MarkAsDeliveredAsync(Guid orderId, CancellationToken cancellationToken);

        // Пометить заказ как завершенный
        Task<bool> MarkAsCompletedAsync(Guid orderId, CancellationToken cancellationToken);

        // Запросить возврат товара у продавца
        Task<bool> RequestReturnAsync(Guid orderId, Guid sellerId, CancellationToken cancellationToken);

        // Отклонить возврат
        Task<bool> RejectReturnAsync(Guid orderId, Guid sellerId, CancellationToken cancellationToken);

        // Одобрить возврат
        Task<bool> ApproveReturnAsync(Guid orderId, Guid sellerId, CancellationToken cancellationToken);

        // Частичный возврат товара
        Task<bool> PartialRefundAsync(Guid orderId, Guid sellerId, Guid productId, int quantity, CancellationToken cancellationToken);

        // Пометить возврат как завершенный (возврат средств)
        Task<bool> MarkAsRefundedAsync(Guid orderId, Guid sellerId, CancellationToken cancellationToken);

        // Снять пометку о возврате (None)
        Task<bool> MarkAsNoneRefundedAsync(Guid orderId, Guid sellerId, CancellationToken cancellationToken);
        */
    }
}
