using Microsoft.EntityFrameworkCore;
using StockService.Domain.Entities;
using StockService.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Infrastructure.Data
{
	public class StockContext : DbContext
	{
		public StockContext(DbContextOptions options) : base(options)
		{
		}

		protected StockContext()
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
		}

		public DbSet<Stock> Stocks { get; set; }

	}
}
