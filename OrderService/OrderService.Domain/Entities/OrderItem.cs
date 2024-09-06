using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
	public class OrderItem :BaseEntity
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }

		public int OrderId { get; set; }  // Foreign key
		public Order Order { get; set; }
	}
}
