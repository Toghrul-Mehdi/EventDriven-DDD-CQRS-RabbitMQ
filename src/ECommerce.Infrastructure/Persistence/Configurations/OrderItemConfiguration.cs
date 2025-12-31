using ECommerce.Domain.Baskets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(oi => oi.OrderId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(oi => oi.ProductId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(oi => oi.ProductName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasIndex(oi => oi.ProductId);
    }
}