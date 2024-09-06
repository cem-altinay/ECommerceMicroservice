﻿using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces
{
	public interface INotificationService
	{
		Task SendNotificationAsync(Notification notification);
	}
}
