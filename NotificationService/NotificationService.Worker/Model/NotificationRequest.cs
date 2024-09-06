using ECommerce.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Worker.Model
{
	public class NotificationRequest
	{
		public string Recipient { get; set; }
		public string Message { get; set; }
		public NotificationType Type { get; set; }
	}
}
