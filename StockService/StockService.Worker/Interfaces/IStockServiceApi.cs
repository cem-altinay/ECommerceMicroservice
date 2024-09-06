using Refit;
using StockService.Worker.Model;


namespace StockService.Worker.Interfaces
{
	public interface IStockServiceApi
	{
		[Post("/Stock/Decrease")]
		Task DecreaseStockAsync([Body] DecreaseStockRequest request);
	}
}
