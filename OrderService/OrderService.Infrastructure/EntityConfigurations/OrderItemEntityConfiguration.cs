using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.EntityConfigurations
{
	internal class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");

			builder.HasKey(a => a.Id);

			builder.Property(oi => oi.UnitPrice)
		   .IsRequired()
		   .HasColumnType("decimal(18,4)");

			builder.HasOne(oi => oi.Order)
			  .WithMany(o => o.OrderItems)
			  .HasForeignKey(oi => oi.OrderId);
		}
	}
}
