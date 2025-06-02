using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        
        Task<Client?> GetClientByUsernameAsync(string username,
            CancellationToken cancellationToken,
            bool asNoTracking = false);

    }
}

