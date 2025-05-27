using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.Repositories.Abstractions;
using MarketPlaceSale.Application.Models.OrderLine;
using MarketPlaceSale.Application.Models.CartLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface IOrderLineApplicationService
    {
        Task<OrderLineModel?> GetOrderLineByIdAsync(Guid id, CancellationToken cancellationToken);

        /*Task<OrderLineModel?> GetOrderLineByUsernameAsync(string username, CancellationToken cancellationToken);*/

        Task<IEnumerable<OrderLineModel>> GetOrderLineAsync(CancellationToken cancellationToken);

        Task<OrderLineModel?> CreateOrderLineAsync(CreateOrderLineModel OrderLineInformation, CancellationToken cancellationToken);

        Task<bool> UpdateOrderLineAsync(OrderLineModel OrderLine, CancellationToken cancellationToken);

        Task<bool> DeleteOrderLineAsync(Guid id, CancellationToken cancellationToken);
    }
}
