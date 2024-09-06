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
	public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");

			builder.HasKey(a => a.Id);

			builder.Property(o => o.CustomerEmail)
			   .IsRequired()
			   .HasMaxLength(256);

			builder.Property(o => o.TotalPrice)
			 .HasColumnType("decimal(18,4)");

			//  (1-N ilişki)
			builder.HasMany(o => o.OrderItems)
				.WithOne(oi => oi.Order)
				.HasForeignKey(oi => oi.OrderId);
		}
	}
}
