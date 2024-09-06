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

		public StockUpdateConsumer(IStockServiceApi stockServiceApi)
		{
			_stockServiceApi = stockServiceApi;
		}

		public async Task Consume(ConsumeContext<StockUpdateMessageEvent> context)
		{
			try
			{
				
				var request = new DecreaseStockRequest
				{
					ProductId = context.Message.ProductId,
					Quantity = context.Message.Quantity
				};

			
				await _stockServiceApi.DecreaseStockAsync(request);
				Console.WriteLine($"Stock updated successfully for Product ID: {request.ProductId}");
			}
			catch (ApiException ex)
			{
				
				Console.WriteLine($"API error: {ex.StatusCode} - {ex.Content}");
			}
			catch (Exception ex)
			{
			
				Console.WriteLine($"An error occurred while updating stock: {ex.Message}");
			}
		}
	}
}
