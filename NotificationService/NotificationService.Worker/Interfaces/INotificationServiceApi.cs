using NotificationService.Worker.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Worker.Interfaces
{
	public interface INotificationServiceApi
	{
		[Post("/Notification/Send")]
		Task DecreaseStockAsync([Body] NotificationRequest request);
	}
}
