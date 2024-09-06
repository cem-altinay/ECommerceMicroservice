using Ardalis.GuardClauses;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IRepository<Notification> _notificationRepository;
		private readonly IEmailSender _emailSender;
		private readonly ISmsSender _smsSender;
		public NotificationService(IRepository<Notification> notificationRepository, IEmailSender emailSender, ISmsSender smsSender)
		{
			_notificationRepository = notificationRepository;
			_emailSender = emailSender;
			_smsSender = smsSender;
		}
		public async Task SendNotificationAsync(Notification notification)
		{
			Guard.Against.Null(notification, nameof(notification));

			await _notificationRepository.AddAsync(notification);

			switch (notification.Type)
			{
				case NotificationType.Email:
					await _emailSender.SendEmailAsync(notification.Recipient, "Notification", notification.Message);
					break;
				case NotificationType.Sms:
					await _smsSender.SendSmsAsync(notification.Recipient, notification.Message);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
