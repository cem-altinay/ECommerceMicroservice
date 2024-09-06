using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Worker.Model
{
	public class DecreaseStockRequest
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
