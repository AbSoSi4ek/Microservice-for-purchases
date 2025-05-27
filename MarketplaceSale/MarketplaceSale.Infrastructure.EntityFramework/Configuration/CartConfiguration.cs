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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Client)
                .WithOne(client => client.Cart) // навигация в Client
                .HasForeignKey<Cart>("ClientId") // shadow FK в Cart
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CartLines)
                .WithOne(cl => cl.Cart)
                .HasForeignKey("CartId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.CartLines)
                .UsePropertyAccessMode(PropertyAccessMode.Field); // читается через _cartLines


            builder.Property<Guid>("ClientId"); // shadow property
        }
    }

}
