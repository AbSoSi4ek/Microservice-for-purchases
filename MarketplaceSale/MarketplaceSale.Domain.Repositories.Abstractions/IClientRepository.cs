using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        
        Task<Client?> GetClientByUsernameAsync(string username, CancellationToken cancellationToken, bool asNoTracking = false);

    }
}
