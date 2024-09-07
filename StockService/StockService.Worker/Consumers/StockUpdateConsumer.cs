using ECommerce.Shared.Contracts;
using MassTransit;
using Refit;
using StockService.Worker.Interfaces;
using StockService.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Worker.Consumers
{
	public class StockUpdateConsumer : IConsumer<StockUpdateMessageEvent>
	{
		private readonly IStockServiceApi _stockServiceApi;
		private readonly ILogger<StockUpdateConsumer> _logger;

		public StockUpdateConsumer(IStockServiceApi stockServiceApi, ILogger<StockUpdateConsumer> logger)
		{
			_stockServiceApi = stockServiceApi;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<StockUpdateMessageEvent> context)
		{
			try
			{
				_logger.LogInformation("Event Context {@Detail}", context?.Message);

				var request = new DecreaseStockRequest
				{
					ProductId = context.Message.ProductId,
					Quantity = context.Message.Quantity
				};

				await _stockServiceApi.DecreaseStockAsync(request);
				Console.WriteLine($"Stock updated successfully for Product ID: {request.ProductId}");
				_logger.LogInformation($"Stock updated successfully for Product ID: {request.ProductId}");
			}
			catch (ApiException ex)
			{
				Console.WriteLine($"API error: {ex.StatusCode} - {ex.Content}");
				_logger.LogError($"API error: {ex.StatusCode} - {ex.Content}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while updating stock: {ex.Message}");
				_logger.LogError($"An error occurred while updating stock: {ex.Message}");
			}
		}
	}
}
