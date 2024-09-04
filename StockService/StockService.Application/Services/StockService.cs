using Ardalis.GuardClauses;
using StockService.Application.Interfaces;
using StockService.Domain.Entities;


namespace StockService.Application.Services
{
	public class StockService : IStockService
	{

		private readonly IRepository<Stock> _stockRepository;

		public StockService(IRepository<Stock> stockRepository)
		{
			_stockRepository = stockRepository;
		}

		public async Task DecreaseStockAsync(int productId, int quantity)
		{
		
			Guard.Against.NegativeOrZero(productId, nameof(productId));

		
			Guard.Against.NegativeOrZero(quantity, nameof(quantity));

		
			var stock = await _stockRepository.GetByIdAsync(productId);
			Guard.Against.Null(stock, nameof(stock), $"Product with id {productId} not found.");

			
			Guard.Against.OutOfRange(quantity, nameof(quantity), 0, stock.Quantity, "Insufficient stock");

			// Stok azaltma işlemi
			stock.Quantity -= quantity;
			await _stockRepository.UpdateAsync(stock);
		}
	}
}
