using AutoMapper;
using MarketplaceSale.Domain.Entities;
using MarketPlaceSale.Application.Models.Client;
using MarketPlaceSale.Application.Services.Abstractions;
using MarketplaceSale.Domain.Enums;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketPlaceSale.Application.Services
{
    public class ClientApplicationService(IClientRepository repository, IMapper mapper) : IClientApplicationService
    {
        // сделать ещё GetClientByUsernameAsync
        public async Task<IEnumerable<ClientModel>> GetClientsAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<ClientModel>);

        public async Task<ClientModel?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Client = await repository.GetByIdAsync(id, cancellationToken);
            return Client is null ? null : mapper.Map<ClientModel>(Client);
        }

        public async Task<ClientModel?> GetClientByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var Client = await repository.GetClientByUsernameAsync(username, cancellationToken);
            return Client is null ? null : mapper.Map<ClientModel>(Client);
        }
        public async Task<ClientModel?> CreateClientAsync(CreateClientModel ClientInformation, CancellationToken cancellationToken = default)
        {
            if (await repository.GetByIdAsync(ClientInformation.Id, cancellationToken) is not null)
                return null;

            Client Client = new(ClientInformation.Id, new Username(ClientInformation.Username));
            var createdClient = await repository.AddAsync(Client, cancellationToken);
            return createdClient is null ? null : mapper.Map<ClientModel>(createdClient);
        }

        public async Task<bool> UpdateClientAsync(ClientModel Client, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(Client.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Client>(Client);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Client = await repository.GetByIdAsync(id, cancellationToken);
            return Client is null ? false : await repository.DeleteAsync(Client, cancellationToken);
        }
    }
}

