using ECommerce.Shared.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Worker.Consumers
{
	public class NotificationConsumer : IConsumer<NotificationEvent>
	{
		public Task Consume(ConsumeContext<NotificationEvent> context)
		{
			throw new NotImplementedException();
		}
	}
}
