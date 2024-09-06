using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Data
{
	public class OrderContext : DbContext
	{
        public OrderContext()
        {
            
        }

		public OrderContext(DbContextOptions<OrderContext> options) : base(options)
		{
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
			modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
		}
	}
}
