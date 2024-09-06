﻿namespace OrderService.API.Model
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
