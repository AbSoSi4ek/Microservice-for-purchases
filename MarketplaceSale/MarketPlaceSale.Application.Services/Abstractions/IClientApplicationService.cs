using MarketPlaceSale.Application.Models.Cart;
using MarketPlaceSale.Application.Models.Client;
using MarketPlaceSale.Application.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Services.Abstractions
{
    public interface IClientApplicationService
    {
        Task<ClientModel?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ClientModel?> GetClientByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IEnumerable<ClientModel>> GetClientsAsync(CancellationToken cancellationToken);

        Task<ClientModel?> CreateClientAsync(CreateClientModel ClientInformation, CancellationToken cancellationToken);

        Task<bool> UpdateClientAsync(ClientModel Client, CancellationToken cancellationToken);

        Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken);



        /*
        // Получение клиента по ID
        Task<ClientModel?> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);

        // Создание нового клиента
        Task<Guid> CreateClientAsync(CreateClientModel model, CancellationToken cancellationToken);
        */
        // Смена имени пользователя
        /*Task<bool> ChangeUsernameAsync(Guid clientId, string newUsername, CancellationToken cancellationToken);*/
        // Работа с балансом
        /*
        Task<decimal> GetBalanceAsync(Guid clientId, CancellationToken cancellationToken);
        Task<bool> AddBalanceAsync(Guid clientId, decimal amount, CancellationToken cancellationToken);
        Task<bool> SubtractBalanceAsync(Guid clientId, decimal amount, CancellationToken cancellationToken);

        // Работа с корзиной (ранее было в ICartApplicationService)
        Task<bool> AddProductToCartAsync(Guid clientId, Guid productId, int quantity, CancellationToken cancellationToken);
        Task<bool> RemoveProductFromCartAsync(Guid clientId, Guid productId, CancellationToken cancellationToken);
        Task<bool> ClearCartAsync(Guid clientId, CancellationToken cancellationToken);

        // Выбор товаров для заказа
        Task<bool> SelectProductAsync(Guid clientId, Guid productId, CancellationToken cancellationToken);
        Task<bool> UnselectProductAsync(Guid clientId, Guid productId, CancellationToken cancellationToken);
        Task<bool> ClearSelectedAsync(Guid clientId, CancellationToken cancellationToken);

        // Получение полной стоимости выбранных товаров
        Task<decimal> GetTotalPriceAsync(Guid clientId, CancellationToken cancellationToken);

        // Заказ
        Task<Guid> PlaceSelectedOrderFromCartAsync(Guid clientId, CancellationToken cancellationToken);
        Task<Guid> PlaceDirectOrderAsync(Guid clientId, Guid productId, int quantity, CancellationToken cancellationToken);
        Task<bool> PayForOrderAsync(Guid clientId, Guid orderId, CancellationToken cancellationToken);
        Task<bool> CancelOrderAsync(Guid clientId, Guid orderId, CancellationToken cancellationToken);

        // Возврат
        Task<bool> RequestReturnAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);
        Task<bool> FinalizeApprovedReturnAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);
        Task<bool> RemoveRejectedReturnAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);
    }
    */
        /*public interface IClientApplicationService
        {
            // Получить информацию о клиенте
            Task<ClientModel?> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);

            // Изменить имя пользователя
            Task<bool> ChangeUsernameAsync(Guid clientId, string newUsername, CancellationToken cancellationToken);

            // Баланс
            Task<decimal> GetAccountBalanceAsync(Guid clientId, CancellationToken cancellationToken);
            Task<bool> AddBalanceAsync(Guid clientId, decimal amount, CancellationToken cancellationToken);
            Task<bool> SubtractBalanceAsync(Guid clientId, decimal amount, CancellationToken cancellationToken);

            // Корзина (делаем делегирование в CartApplicationService, если нужно)
            Task<CartModel?> GetCartAsync(Guid clientId, CancellationToken cancellationToken);
            Task<bool> AddProductToCartAsync(Guid clientId, Guid productId, int quantity, CancellationToken cancellationToken);
            Task<bool> RemoveProductFromCartAsync(Guid clientId, Guid productId, CancellationToken cancellationToken);
            Task<bool> ClearCartAsync(Guid clientId, CancellationToken cancellationToken);

            // Заказы
            Task<OrderModel> PlaceSelectedOrderFromCartAsync(Guid clientId, CancellationToken cancellationToken);
            Task<OrderModel> PlaceDirectOrderAsync(Guid clientId, Guid productId, int quantity, CancellationToken cancellationToken);
            Task<bool> PayForOrderAsync(Guid clientId, Guid orderId, CancellationToken cancellationToken);
            Task<bool> CancelOrderAsync(Guid clientId, Guid orderId, CancellationToken cancellationToken);

            // Возвраты
            Task<bool> RequestReturnOrderAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);
            Task<bool> FinalizeApprovedReturnAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);
            Task<bool> RemoveRejectedReturnAsync(Guid clientId, Guid orderId, Guid sellerId, CancellationToken cancellationToken);

            // Истории заказов и возвратов
            Task<IReadOnlyCollection<OrderModel>> GetPurchaseHistoryAsync(Guid clientId, CancellationToken cancellationToken);
            Task<IReadOnlyCollection<OrderModel>> GetReturnHistoryAsync(Guid clientId, CancellationToken cancellationToken);
        }*/
    }
}