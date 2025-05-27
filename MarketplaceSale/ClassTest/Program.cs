using System;
using System.Linq;
using MarketplaceSale.Domain.Entities;
using MarketplaceSale.Domain.ValueObjects;

namespace ClassTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Создаём клиента
                Guid clientId = Guid.NewGuid();
                Username username1 = new("Иванов Иван");
                Client client = new(clientId, username1);

                // Создаём продавцов
                Guid sellerId1 = Guid.NewGuid();
                Username username2 = new("Продавец 1");
                Seller seller1 = new(sellerId1, username2);

                Guid sellerId2 = Guid.NewGuid();
                Username username3 = new("Продавец 2");
                Seller seller2 = new(sellerId2, username3);

                // Создаём продукты
                ProductName phoneName = new("Iphone 16");
                ProductName caseName = new("Чехол для Iphone 16");

                Description phoneDescription = new("Самый скучный смартфон");
                Description caseDescription = new("Самый скучный чехол");

                Money price1 = new(999);
                Money price2 = new(150);

                Quantity quantity1 = new(10);
                Quantity quantity2 = new(10);

                Product iphone16 = new(phoneName, phoneDescription, price1, quantity1, seller1);
                Product iphoneCase = new(caseName, caseDescription, price2, quantity2, seller2);

                seller1.AddProduct(iphone16);
                seller2.AddProduct(iphoneCase);

                Console.WriteLine("Товары добавлены в магазин продавцами.");

                // Создаём корзину для клиента
                Cart cart = new(client);

                Console.WriteLine($"Создана корзина для клиента {client.Username} (ID: {client.Id})");

                // Добавляем товары в корзину
                client.AddToCart(iphone16, new Quantity(3));
                client.AddToCart(iphoneCase, new Quantity(2));

                // Выводим содержимое корзины
                PrintCart(client);

                client.RemoveFromCart(iphoneCase);
                Console.WriteLine("Клиент удалил чехол из корзины.");

                PrintCart(client);

                // Очищаем корзину
                Console.WriteLine("\nОчищаем корзину...");
                client.ClearCart();

                PrintCart(client);

                client.AddToCart(iphone16, new Quantity(2));
                client.AddToCart(iphoneCase, new Quantity(1));
                PrintCart(client);

                //пополнение счёта
                client.AddBalance(new Money(5000));
                Console.WriteLine($"Баланс клиента после пополнения: {client.AccountBalance}");

                client.SelectProductForOrder(iphone16);
                client.PlaceSelectedOrderFromCart();
                Console.WriteLine("Клиент оформил заказ из корзины.");

                client.PlaceDirectOrder(iphoneCase, new Quantity(1));
                Console.WriteLine("Клиент оформил заказ напрямую.");


                //потом менять статусы типа доставка, после деливер сделать два возврата, в одном продавец соглашается, в другом нет

                // Локальная функция для вывода корзины
                void PrintCart(Client client)
                {
                    Console.WriteLine($"\nКорзина клиента {client.Username}:");
                    if (!client.Cart.CartLines.Any())
                    {
                        Console.WriteLine("Корзина пуста.");
                        return;
                    }

                    foreach (var line in client.Cart.CartLines)
                    {
                        Console.WriteLine($"- {line.Product.ProductName} x {line.Quantity} = {line.GetPrice()}");
                    }

                    Console.WriteLine($"Общая сумма: {client.Cart.GetTotalPrice()}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            
        }
    }
}
