using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Worker.Model
{
	public class RabbitMQSettings
	{
		public string Host { get; set; }
		public string VirtualHost { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int Port { get; set; }
	}
}
