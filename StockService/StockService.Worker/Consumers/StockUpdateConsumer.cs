using ECommerce.Shared.Contracts;
using MassTransit;
using Polly;
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


			_logger.LogInformation("Event Context {@Detail}", context?.Message);

			// Polly retry politikası
			var retryPolicy = Policy
				.Handle<Exception>()
				.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5), (exception, timeSpan, retryCount, ctx) =>
				{
					Console.WriteLine($"Retry {retryCount} implemented due to: {exception.Message}");
				});

			// Fallback: Retry sonrasında başarısız olursa
			var fallbackPolicy = Policy
				.Handle<Exception>()
				.FallbackAsync(async (ct) =>
				{
					Console.WriteLine("Retry işlemi başarısız oldu, mesaj error queue'ya taşınıyor.");
					// Fallback işlemi: Mesajı error kuyruğa taşı
					throw new InvalidOperationException("Mesaj retry işlemi sonrasında başarısız oldu");
				});

			// Retry ve fallback politikalarını birlikte sarıyoruz
			var combinedPolicy = Policy.WrapAsync(retryPolicy, fallbackPolicy);

			// Polly ile işlemi sarmalıyoruz
			await combinedPolicy.ExecuteAsync(async () =>
			{
				var request = new DecreaseStockRequest
				{
					ProductId = context.Message.ProductId,
					Quantity = context.Message.Quantity
				};
				await _stockServiceApi.DecreaseStockAsync(request);


				Console.WriteLine($"Stock updated successfully for Product ID: {request.ProductId}");
				_logger.LogInformation($"Stock updated successfully for Product ID: {request.ProductId}");
			});

		}
	}
}
