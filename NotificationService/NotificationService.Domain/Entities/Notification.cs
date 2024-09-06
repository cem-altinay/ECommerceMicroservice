using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
	public class Notification :BaseEntity
	{	
		public string Recipient { get; set; }
		public string Message { get; set; }
		public NotificationType Type { get; set; }
		public DateTime SentDate { get; set; }
	}
}
