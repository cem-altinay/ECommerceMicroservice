using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Application.Interfaces
{
	public interface IStockService
	{
		Task DecreaseStockAsync(int productId, int quantity);
	}
}
