using AutoMapper;
using MarketPlaceSale.Application.Models.Seller;
using MarketPlaceSale.Application.Services.Abstractions;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Repositories.Abstractions;

namespace MarketPlaceSale.Application.Services
{
    public class SellerApplicationService(ISellerRepository repository, IMapper mapper) : ISellerApplicationService
    {
        public async Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<SellerModel>);

        public async Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Seller = await repository.GetByIdAsync(id, cancellationToken);
            return Seller is null ? null : mapper.Map<SellerModel>(Seller);
        }

        public async Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var Seller = await repository.GetSellerByUsernameAsync(username, cancellationToken);
            return Seller is null ? null : mapper.Map<SellerModel>(Seller);
        }
        public async Task<SellerModel?> CreateSellerAsync(CreateSellerModel SellerInformation, CancellationToken cancellationToken = default)
        {
            if (await repository.GetByIdAsync(SellerInformation.Id, cancellationToken) is not null)
                return null;

            Seller Seller = new(SellerInformation.Id, new Username(SellerInformation.Username));
            var createdSeller = await repository.AddAsync(Seller, cancellationToken);
            return createdSeller is null ? null : mapper.Map<SellerModel>(createdSeller);
        }

        public async Task<bool> UpdateSellerAsync(SellerModel Seller, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(Seller.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Seller>(Seller);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Seller = await repository.GetByIdAsync(id, cancellationToken);
            return Seller is null ? false : await repository.DeleteAsync(Seller, cancellationToken);
        }
    }
}
