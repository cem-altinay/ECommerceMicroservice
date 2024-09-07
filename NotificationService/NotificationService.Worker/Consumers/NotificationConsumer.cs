using ECommerce.Shared.Contracts;
using MassTransit;
using NotificationService.Worker.Interfaces;
using NotificationService.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Worker.Consumers
{
	public class NotificationConsumer : IConsumer<NotificationEvent>
	{
		private readonly INotificationServiceApi _notificationServiceApi;
		private readonly ILogger<NotificationConsumer> _logger;

		public NotificationConsumer(INotificationServiceApi notificationServiceApi, ILogger<NotificationConsumer> logger)
		{
			_notificationServiceApi = notificationServiceApi;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<NotificationEvent> context)
		{
			try
			{
				var notificationEvent = context.Message;

				await _notificationServiceApi.NotificationSendAsync(new NotificationRequest
				{
					Recipient = notificationEvent.Recipient,
					Message = notificationEvent.Message,
					Type = (int)notificationEvent.Type
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while consuming the notification event.");
				throw; 
			}
		}
	}
}
