using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Infrastructure.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new StockContext(serviceProvider.GetRequiredService<DbContextOptions<StockContext>>()))
			{
				context.Database.Migrate(); 

				if (context.Stocks.Any())
				{
					return;
				}

				Random random = new Random();

				for (int i = 0; i < 1000; i++)
				{
					context.Stocks.Add(new Stock
					{
						ProductName = "Product " + i,
						Quantity = random.Next(1, 1000),
						Price = random.Next(1, 1000),
					});
				}

				context.SaveChanges(); 
			}

		}
	}
}
