using AutoMapper;
using MarketplaceSale.Domain.Repositories.Abstractions;
using MarketPlaceSale.Application.Models.Product;
using MarketPlaceSale.Application.Services.Abstractions;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketPlaceSale.Application.Services
{
    public class ProductApplicationService(IProductRepository repository,
        ISellerRepository SellerRepository, 
        IMapper mapper) : IProductApplicationService
    {
        public async Task<IEnumerable<ProductModel>> GetProductAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<ProductModel>);

        public async Task<ProductModel?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Product = await repository.GetByIdAsync(id, cancellationToken);
            return Product is null ? null : mapper.Map<ProductModel>(Product);
        }

        public async Task<ProductModel?> CreateProductAsync(CreateProductModel ProductInformation, CancellationToken cancellationToken = default)
        {
            var Seller = await SellerRepository.GetByIdAsync(ProductInformation.SellerId, cancellationToken);
            if (Seller == null)
                return null;

            if (await repository.GetByIdAsync(ProductInformation.Id, cancellationToken) is not null)
                return null;

            var productName = new ProductName(ProductInformation.ProductName);
            var description = new Description(ProductInformation.Description);
            var price = new Money(ProductInformation.Price);
            var quantity = new Quantity(ProductInformation.StockQuantity);

            var product = new Product(productName, description, price, quantity, Seller);

            Product Product = new(productName, description, price, quantity, Seller);
            var createdProduct = await repository.AddAsync(Product, cancellationToken);
            return createdProduct is null ? null : mapper.Map<ProductModel>(createdProduct);
        }

        public async Task<bool> UpdateProductAsync(ProductModel Product, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(Product.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Product>(Product);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Product = await repository.GetByIdAsync(id, cancellationToken);
            return Product is null ? false : await repository.DeleteAsync(Product, cancellationToken);
        }
    }
}
