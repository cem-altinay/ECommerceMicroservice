using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Infrastructure.EntityConfigurations
{
	public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
	{
		public void Configure(EntityTypeBuilder<Stock> builder)
		{
			builder.ToTable("Stocks");

			builder.HasKey(a => a.Id);
		}
	}
}
