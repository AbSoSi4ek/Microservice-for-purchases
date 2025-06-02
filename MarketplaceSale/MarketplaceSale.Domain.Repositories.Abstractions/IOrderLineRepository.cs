using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface IOrderLineRepository : IRepository<OrderLine, Guid>
    { }
}


    //{
    //    // Получить OrderLine по ID продукта (предполагается, что OrderLine уникально идентифицируется продуктом в рамках заказа)
    //    Task<OrderLine?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    //    // Добавить новую строку заказа
    //    Task AddAsync(OrderLine orderLine, CancellationToken cancellationToken = default);

    //    // Обновить строку заказа (например, изменить количество)
    //    Task<bool> UpdateAsync(OrderLine orderLine, CancellationToken cancellationToken = default);

    //    // Удалить строку заказа
    //    Task<bool> DeleteAsync(OrderLine orderLine, CancellationToken cancellationToken = default);

    //    // Проверка существования строки заказа по ID продукта
    //    Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default);
    //}

