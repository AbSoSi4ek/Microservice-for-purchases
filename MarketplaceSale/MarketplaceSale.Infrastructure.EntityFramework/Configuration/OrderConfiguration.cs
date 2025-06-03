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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasOne(o => o.Client)
                   .WithMany("_purchaseHistory")
                   .HasForeignKey("ClientId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.ClientReturning)
                   .WithMany("_returnHistory")
                   .HasForeignKey("ClientReturningId")
                   .OnDelete(DeleteBehavior.Restrict);

            /*builder.HasOne(o => o.Seller)
                   .WithMany("_salesHistory")
                   .HasForeignKey("SellerId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);*/

            builder.Property(o => o.TotalAmount)
                   .HasConversion(
                       money => money.Value,
                       value => new Money(value))
                   .IsRequired();

            builder.Property(o => o.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(o => o.OrderDate)
                   .HasConversion(
                       od => od.Value,
                       value => new OrderDate(value))
                   .IsRequired();

            builder.Property(o => o.DeliveryDate)
                   .HasConversion(
                       dd => dd == null ? (DateTime?)null : dd.Value,
                       value => value == null ? null : new DeliveryDate(value.Value))
                   .IsRequired(false);

            builder.HasMany<OrderLine>("_orderLines")
               .WithOne(ol => ol.Order)
               .HasForeignKey(ol => ol.OrderId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);



            builder.Navigation("_orderLines")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(o => o.OrderLines);

            // Сложные словари можно игнорировать и сохранять отдельно (если хочешь)
            builder.Ignore(o => o.ReturnedProducts);
            builder.Ignore(o => o.ReturnStatuses);
        }
    }
}
