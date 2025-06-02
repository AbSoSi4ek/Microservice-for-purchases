using MarketPlaceSale.Application.Models.Product;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface IProductApplicationService
    {
        Task<ProductModel?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductModel>> GetProductAsync(CancellationToken cancellationToken);
        Task<ProductModel?> CreateProductAsync(CreateProductModel ProductInformation, CancellationToken cancellationToken);
        Task<bool> UpdateProductAsync(ProductModel Product, CancellationToken cancellationToken);
        Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}



/*
// Получить все товары
Task<IEnumerable<ProductModel>> GetAllProductsAsync();

// Получить товар по ID
Task<ProductModel?> GetProductByIdAsync(Guid productId);

// Создать товар (с привязкой к продавцу)
Task<Guid> CreateProductAsync(ProductModel model);

// Обновить описание и цену товара (только продавец может)
Task UpdateProductAsync(Guid productId, string newDescription, decimal newPrice, Guid sellerId);

// Удалить товар (если поддерживается)
Task DeleteProductAsync(Guid productId, Guid sellerId);

// Изменить количество на складе (увеличение/уменьшение) продавцом
Task IncreaseStockAsync(Guid productId, int quantity, Guid sellerId);
Task DecreaseStockAsync(Guid productId, int quantity, Guid sellerId);

// Обновление склада при заказе/возврате
Task RemoveStockByOrderAsync(Guid productId, int quantity, Guid sellerId);
Task ReturnStockByOrderAsync(Guid productId, int quantity, Guid sellerId);
*/