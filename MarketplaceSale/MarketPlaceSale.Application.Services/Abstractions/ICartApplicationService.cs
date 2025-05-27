using MarketPlaceSale.Application.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface ICartApplicationService
    {
        Task<CartModel?> GetCartByIdAsync(Guid id, CancellationToken cancellationToken);

        /*Task<CartModel?> GetCartByUsernameAsync(string username, CancellationToken cancellationToken);*/

        Task<IEnumerable<CartModel>> GetCartAsync(CancellationToken cancellationToken);

        Task<CartModel?> CreateCartAsync(CreateCartModel CartInformation, CancellationToken cancellationToken);

        Task<bool> UpdateCartAsync(CartModel Cart, CancellationToken cancellationToken);

        Task<bool> DeleteCartAsync(Guid id, CancellationToken cancellationToken);

        /*
        Task<CartModel?> GetCartByCartIdAsync(Guid CartId, CancellationToken cancellationToken);

        Task<bool> AddProductToCartAsync(Guid CartId, Guid productId, int quantity, CancellationToken cancellationToken);

        Task<bool> RemoveProductFromCartAsync(Guid CartId, Guid productId, CancellationToken cancellationToken);

        Task<bool> SelectProductAsync(Guid CartId, Guid productId, CancellationToken cancellationToken);

        Task<bool> UnselectProductAsync(Guid CartId, Guid productId, CancellationToken cancellationToken);

        Task<bool> ClearSelectedAsync(Guid CartId, CancellationToken cancellationToken);

        Task<bool> ClearCartAsync(Guid CartId, CancellationToken cancellationToken);

        Task<decimal> GetTotalPriceAsync(Guid CartId, CancellationToken cancellationToken);
        */
    }
}
