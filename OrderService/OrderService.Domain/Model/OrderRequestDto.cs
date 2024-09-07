using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model
{
	public class OrderRequestDto
	{
		[Required]
		[EmailAddress]
		public string CustomerEmail { get; set; }

		[Required]
		public List<OrderItemDto> OrderItems { get; set; }

		public class OrderItemDto
		{
			[Required]
			public string ProductName { get; set; }
			public int ProductId { get; set; }

			[Required]
			[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
			public int Quantity { get; set; }

			[Required]
			[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
			public decimal UnitPrice { get; set; }
		}
	}
}
