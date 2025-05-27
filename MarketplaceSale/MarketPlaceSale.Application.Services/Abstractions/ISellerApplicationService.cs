using MarketPlaceSale.Application.Models.Product;
using MarketPlaceSale.Application.Models.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface ISellerApplicationService
    {
        Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken);

        Task<SellerModel?> CreateSellerAsync(CreateSellerModel SellerInformation, CancellationToken cancellationToken);

        Task<bool> UpdateSellerAsync(SellerModel Seller, CancellationToken cancellationToken);

        Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken);

        /*
        // Получить информацию о продавце
        Task<SellerModel> GetByIdAsync(Guid sellerId);

        // Зарегистрировать нового продавца
        Task<Guid> RegisterSellerAsync(string username);

        // Изменить имя продавца
        Task ChangeUsernameAsync(Guid sellerId, string newUsername);

        // Добавить продукт
        Task AddProductAsync(Guid sellerId, ProductModel productModel);

        // Удалить продукт
        Task RemoveProductAsync(Guid sellerId, Guid productId);

        // Изменить цену продукта
        Task ChangeProductPriceAsync(Guid sellerId, Guid productId, decimal newPrice);

        // Пополнить наличие продукта
        Task ReplenishProductAsync(Guid sellerId, Guid productId, int quantity);

        // Уменьшить наличие продукта
        Task ReduceProductStockAsync(Guid sellerId, Guid productId, int quantity);

        // Одобрить возврат
        Task ApproveOrderReturnAsync(Guid sellerId, Guid orderId);

        // Отклонить возврат
        Task RejectOrderReturnAsync(Guid sellerId, Guid orderId);

        // Частичный возврат
        Task ProcessPartialRefundAsync(Guid sellerId, Guid orderId, Guid productId, int quantity);

        // Полный возврат
        Task ProcessFullRefundAsync(Guid sellerId, Guid orderId);
        */
    }

}
