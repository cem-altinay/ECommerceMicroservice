using ECommerce.Shared.Contracts;
using MassTransit;
using StockService.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Worker.Consumers
{
	public class StockUpdateErrorConsumer : IConsumer<Fault<StockUpdateMessageEvent>>
	{
		private readonly ILogger<StockUpdateErrorConsumer> _logger;

		public StockUpdateErrorConsumer(ILogger<StockUpdateErrorConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<Fault<StockUpdateMessageEvent>> context)
		{


			var failedMessage = context.Message.Message;
			var exceptionInfo = context.Message.Exceptions;


			_logger.LogError($"Hata Alan Mesaj: ProductId={failedMessage.ProductId}, Quantity={failedMessage.Quantity}");

			foreach (var exception in exceptionInfo)
			{
				Console.WriteLine($"Hata: {exception.Message}");
			}

			var notificationEmailEvent = new NotificationEvent
			{
				Recipient = "admin@test.com",
				Type = NotificationType.Email,
				Message = $"Failed to update stock for Product ID: {failedMessage?.ProductId}"
			};
			await context.Publish(notificationEmailEvent);

			var notificationSmsEvent = new NotificationEvent
			{
				Recipient = "admin@test.com",
				Type = NotificationType.Sms,
				Message = $"Failed to update stock for Product ID: {failedMessage?.ProductId}"
			};
			await context.Publish(notificationSmsEvent);
		}
	}
}
