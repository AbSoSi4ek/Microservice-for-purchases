using MarketplaceSale.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects;

namespace MarketplaceSale.Infrastructure.EntityFramework.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Username - ValueObject с конвертацией в строку
            builder.Property(c => c.Username)
                .IsRequired()
                .HasConversion(
                    u => u.Value,
                    v => new Username(v))
                .HasMaxLength(50);

            // AccountBalance - ValueObject с конвертацией в decimal
            builder.Property(c => c.AccountBalance)
                .IsRequired()
                .HasConversion(
                    m => m.Value,
                    v => new Money(v));

            // Cart - связь один к одному
            builder.HasOne(c => c.Cart)
                .WithOne(cart => cart.Client)  // Cart должен иметь навигацию к Client
                .HasForeignKey<Cart>("ClientId") // shadow property в Cart
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // История покупок — заказы, которые сделал клиент
            builder.HasMany<Order>("_purchaseHistory")
                .WithOne(o => o.Client)
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);

            // История возвратов — заказы, где клиент был возвратчиком
            builder.HasMany<Order>("_returnHistory")
                .WithOne(o => o.ClientReturning)
                .HasForeignKey("ClientReturningId")
                .OnDelete(DeleteBehavior.Restrict);

            // Указываем EF, что коллекции - это поля с доступом через field
            builder.Navigation("_purchaseHistory").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation("_returnHistory").UsePropertyAccessMode(PropertyAccessMode.Field);

            // Игнорируем публичные свойства, чтобы не было конфликтов
            builder.Ignore(c => c.PurchaseHistory);
            builder.Ignore(c => c.ReturnHistory);
        }
    }

}
