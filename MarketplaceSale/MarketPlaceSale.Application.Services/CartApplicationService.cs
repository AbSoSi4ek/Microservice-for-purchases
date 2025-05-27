using MarketPlaceSale.Application.Models.Cart;
using MarketPlaceSale.Application.Services.Abstractions;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Repositories.Abstractions;
using AutoMapper;

namespace MarketPlaceSale.Application.Services
{
    public class CartApplicationService(ICartRepository repository, IClientRepository clientRepository, IMapper mapper) : ICartApplicationService
    {
        public async Task<IEnumerable<CartModel>> GetCartAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<CartModel>);

        public async Task<CartModel?> GetCartByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Cart = await repository.GetByIdAsync(id, cancellationToken);
            return Cart is null ? null : mapper.Map<CartModel>(Cart);
        }

        // сделать ещё ничего пока что 

        public async Task<CartModel?> CreateCartAsync(CreateCartModel CartInformation, CancellationToken cancellationToken = default)
        {
            var client = await clientRepository.GetByIdAsync(CartInformation.ClientId, cancellationToken);
            if (client == null)
            {
                return null;
            }
            if (await repository.GetByIdAsync(CartInformation.Id, cancellationToken) is not null)
                return null;

            Cart Cart = new(client);
            var createdCart = await repository.AddAsync(Cart, cancellationToken);
            return createdCart is null ? null : mapper.Map<CartModel>(createdCart);
        }

        public async Task<bool> UpdateCartAsync(CartModel Cart, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(Cart.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Cart>(Cart);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteCartAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Cart = await repository.GetByIdAsync(id, cancellationToken);
            return Cart is null ? false : await repository.DeleteAsync(Cart, cancellationToken);
        }
    }
}
