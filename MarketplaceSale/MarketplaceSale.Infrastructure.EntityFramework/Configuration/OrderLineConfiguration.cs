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
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(ol => ol.Id);
            builder.Property(ol => ol.Id).ValueGeneratedOnAdd();

            builder.HasOne(ol => ol.Product)
                   .WithMany()
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ol => ol.Seller)
                   .WithMany()
                   .HasForeignKey("SellerId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Order>()
                   .WithMany("_orderLines")
                   .HasForeignKey("OrderId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            // Правильное маппинг value object'а
            builder.Property(ol => ol.Quantity)
                .HasConversion(
                    q => q.Value,
                    v => new Quantity(v))
                .IsRequired();
            

        }
    }

}
