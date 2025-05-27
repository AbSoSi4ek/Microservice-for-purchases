using AutoMapper;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.Repositories.Abstractions;
using MarketplaceSale.Domain.ValueObjects;
using MarketPlaceSale.Application.Models.Cart;
using MarketPlaceSale.Application.Models.CartLine;
using MarketPlaceSale.Application.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services
{
    public class CartLineApplicationService(IRepository<CartLine, Guid> repository, IClientRepository clientRepository, IProductRepository productRepository, IMapper mapper) : ICartLineApplicationService
    {
        public async Task<IEnumerable<CartLineModel>> GetCartLineAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<CartLineModel>);

        public async Task<CartLineModel?> CreateCartLineAsync(Guid cartId, CreateCartLineModel cartLineInformation, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(cartLineInformation.ProductId, cancellationToken);
            if (product is null)
                return null;

            if (await repository.GetByIdAsync(cartLineInformation.Id, cancellationToken) is not null)
                return null;

            var quantity = new Quantity(cartLineInformation.Quantity);
            var cartLine = new CartLine(product, quantity);

            var createdCartLine = await repository.AddAsync(cartLine, cancellationToken);
            return createdCartLine is null ? null : mapper.Map<CartLineModel>(createdCartLine);
        }

        public async Task<CartLineModel?> GetCartLineByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cartLine = await repository.GetByIdAsync(id, cancellationToken);
            return cartLine is null ? null : mapper.Map<CartLineModel>(cartLine);
        }

        /*public async Task<CartLineModel?> GetCartLineByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var CartLine = await repository.GetCartLineByUsernameAsync(username, cancellationToken);
            return CartLine is null ? null : mapper.Map<CartLineModel>(CartLine);
        }*/
        public async Task<CartLineModel?> CreateCartLineAsync(CreateCartLineModel CartLineInformation, CancellationToken cancellationToken = default)
        {
            var client = await clientRepository.GetByIdAsync(CartLineInformation.Id, cancellationToken);
            if (client == null)
            {
                return null;
            }
            if (await repository.GetByIdAsync(CartLineInformation.Id, cancellationToken) is not null)
                return null;

            var product = await productRepository.GetByIdAsync(CartLineInformation.ProductId, cancellationToken);
            if (product == null)
            {
                return null;
            }

            var quantity = new Quantity(CartLineInformation.Quantity);

            CartLine CartLine = new(product, quantity);
            var createdCartLine = await repository.AddAsync(CartLine, cancellationToken);
            return createdCartLine is null ? null : mapper.Map<CartLineModel>(createdCartLine);
        }

        public async Task<bool> UpdateCartLineAsync(CartLineModel CartLine, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(CartLine.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<CartLine>(CartLine);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteCartLineAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var CartLine = await repository.GetByIdAsync(id, cancellationToken);
            return CartLine is null ? false : await repository.DeleteAsync(CartLine, cancellationToken);
        }
    }
}
