using NotificationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Sms
{
	public class SmsSender : ISmsSender
	{
		public Task SendSmsAsync(string to, string message)
		{
            Console.WriteLine("SMS gönderimi sağlandı");
			return Task.CompletedTask;
		}
	}
}
