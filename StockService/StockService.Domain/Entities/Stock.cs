using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Domain.Entities
{
	public partial class Stock : BaseEntity
	{	
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
        public DateTime? UpdatedDateUtc { get; set; } 

        public void DecreaseStock(int quantity)
		{
			if (Quantity < quantity)
				throw new InvalidOperationException("Insufficient stock");

			Quantity -= quantity;
		}
	}
}
