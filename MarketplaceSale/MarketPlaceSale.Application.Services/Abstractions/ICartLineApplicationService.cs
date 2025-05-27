using MarketPlaceSale.Application.Models.CartLine;
using MarketPlaceSale.Application.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface ICartLineApplicationService
    {

        Task<CartLineModel?> GetCartLineByIdAsync(Guid id, CancellationToken cancellationToken);

        /*Task<CartLineModel?> GetCartLineByUsernameAsync(string username, CancellationToken cancellationToken);*/

        Task<IEnumerable<CartLineModel>> GetCartLineAsync(CancellationToken cancellationToken);

        Task<CartLineModel?> CreateCartLineAsync(CreateCartLineModel CartLineInformation, CancellationToken cancellationToken);

        Task<bool> UpdateCartLineAsync(CartLineModel CartLine, CancellationToken cancellationToken);

        Task<bool> DeleteCartLineAsync(Guid id, CancellationToken cancellationToken);

    }
}
