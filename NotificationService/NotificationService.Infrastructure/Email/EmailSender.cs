using NotificationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Email
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string to, string subject, string body)
		{
            Console.WriteLine("Email gönderimi sağlandı");
			return Task.CompletedTask;
		}
	}
}
