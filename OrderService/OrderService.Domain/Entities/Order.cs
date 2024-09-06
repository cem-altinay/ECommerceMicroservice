using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
	public class Order :BaseEntity
	{
        public string CustomerEmail { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public List<OrderItem> OrderItems { get; set; } = [];
	}
}
