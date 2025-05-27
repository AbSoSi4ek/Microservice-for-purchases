using AutoMapper;
using MarketplaceSale.Domain.Repositories.Abstractions;
using MarketPlaceSale.Application.Models.Order;
using MarketPlaceSale.Application.Services.Abstractions;
using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services
{
    public class OrderApplicationService(IOrderRepository repository, IProductRepository productRepository, IClientRepository clientRepository, IMapper mapper) : IOrderApplicationService
    {
        // добавить GetByOrderIdAsync
        public async Task<IEnumerable<OrderModel>> GetOrderAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<OrderModel>);

        public async Task<OrderModel?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Order = await repository.GetByIdAsync(id, cancellationToken);
            return Order is null ? null : mapper.Map<OrderModel>(Order);
        }

        /*public async Task<OrderModel?> GetOrderByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var Order = await repository.GetOrderByUsernameAsync(username, cancellationToken);
            return Order is null ? null : mapper.Map<OrderModel>(Order);
        }*/
        public async Task<OrderModel?> CreateOrderAsync(CreateOrderModel OrderInformation, CancellationToken cancellationToken = default)
        {
            var client = await clientRepository.GetByIdAsync(OrderInformation.ClientId, cancellationToken);
            if (client == null)
            {
                return null;
            }
            if (await repository.GetByIdAsync(OrderInformation.Id, cancellationToken) is not null)
                return null;

            var products = await productRepository.GetAllAsync(cancellationToken);
            if (products == null)
            {
                return null;
            }

            Order Order = new(client, products);
            var createdOrder = await repository.AddAsync(Order, cancellationToken);
            return createdOrder is null ? null : mapper.Map<OrderModel>(createdOrder);
        }

        public async Task<bool> UpdateOrderAsync(OrderModel Order, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(Order.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Order>(Order);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Order = await repository.GetByIdAsync(id, cancellationToken);
            return Order is null ? false : await repository.DeleteAsync(Order, cancellationToken);
        }
    }
}
