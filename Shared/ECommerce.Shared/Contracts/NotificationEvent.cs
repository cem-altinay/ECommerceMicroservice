using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Contracts
{
	public class NotificationEvent
	{
		public string Recipient { get; set; }
		public string Message { get; set; }
		public NotificationType Type { get; set; }
	}

	public enum NotificationType
	{
		Email,
		Sms
	}
}
