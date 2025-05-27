using AutoMapper;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.Repositories.Abstractions;
using MarketplaceSale.Domain.ValueObjects;
using MarketPlaceSale.Application.Models.Order;
using MarketPlaceSale.Application.Models.OrderLine;
using MarketPlaceSale.Application.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services
{
    public class OrderLineApplicationService(IRepository<OrderLine, Guid> repository, IClientRepository clientRepository, IProductRepository productRepository, IMapper mapper) : IOrderLineApplicationService
    {
        public async Task<IEnumerable<OrderLineModel>> GetOrderLineAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<OrderLineModel>);

        public async Task<OrderLineModel?> CreateOrderLineAsync(Guid cartId, CreateOrderLineModel OrderLineInformation, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(OrderLineInformation.ProductId, cancellationToken);
            if (product is null)
                return null;

            if (await repository.GetByIdAsync(OrderLineInformation.Id, cancellationToken) is not null)
                return null;

            var quantity = new Quantity(OrderLineInformation.Quantity);
            var OrderLine = new OrderLine(product, quantity);

            var createdOrderLine = await repository.AddAsync(OrderLine, cancellationToken);
            return createdOrderLine is null ? null : mapper.Map<OrderLineModel>(createdOrderLine);
        }

        public async Task<OrderLineModel?> GetOrderLineByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var OrderLine = await repository.GetByIdAsync(id, cancellationToken);
            return OrderLine is null ? null : mapper.Map<OrderLineModel>(OrderLine);
        }

        /*public async Task<OrderLineModel?> GetOrderLineByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var OrderLine = await repository.GetOrderLineByUsernameAsync(username, cancellationToken);
            return OrderLine is null ? null : mapper.Map<OrderLineModel>(OrderLine);
        }*/
        public async Task<OrderLineModel?> CreateOrderLineAsync(CreateOrderLineModel OrderLineInformation, CancellationToken cancellationToken = default)
        {
            var client = await clientRepository.GetByIdAsync(OrderLineInformation.Id, cancellationToken);
            if (client == null)
            {
                return null;
            }
            if (await repository.GetByIdAsync(OrderLineInformation.Id, cancellationToken) is not null)
                return null;

            var product = await productRepository.GetByIdAsync(OrderLineInformation.ProductId, cancellationToken);
            if (product == null)
            {
                return null;
            }

            var quantity = new Quantity(OrderLineInformation.Quantity);

            OrderLine OrderLine = new(product, quantity);
            var createdOrderLine = await repository.AddAsync(OrderLine, cancellationToken);
            return createdOrderLine is null ? null : mapper.Map<OrderLineModel>(createdOrderLine);
        }

        public async Task<bool> UpdateOrderLineAsync(OrderLineModel OrderLine, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(OrderLine.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<OrderLine>(OrderLine);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteOrderLineAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var OrderLine = await repository.GetByIdAsync(id, cancellationToken);
            return OrderLine is null ? false : await repository.DeleteAsync(OrderLine, cancellationToken);
        }
    }
}
