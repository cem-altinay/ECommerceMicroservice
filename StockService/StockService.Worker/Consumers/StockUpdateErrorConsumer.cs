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
	public class StockUpdateErrorConsumer : IConsumer<StockUpdateMessageEvent>
	{
		public async Task Consume(ConsumeContext<StockUpdateMessageEvent> context)
		{
			//Gelen veriyi db yada ayrı bir noktada loglama işlemi yapılabilir.
			Console.WriteLine($"Error processing stock update for Product ID: {context.Message.ProductId}. Message will be logged or further analyzed.");

		
			var notificationEmailEvent = new NotificationEvent
			{
				Recipient = "admin@test.com",
				Type = NotificationType.Email,
				Message = $"Failed to update stock for Product ID: {context.Message.ProductId}"
			};
			await context.Publish(notificationEmailEvent);

			var notificationSmsEvent = new NotificationEvent
			{
				Recipient = "admin@test.com",
				Type = NotificationType.Sms,
				Message = $"Failed to update stock for Product ID: {context.Message.ProductId}"
			};
			await context.Publish(notificationSmsEvent);
		}
	}
}
