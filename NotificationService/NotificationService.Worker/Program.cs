using ECommerce.Shared.Utilities.Loging.Serilog;
using MassTransit;
using NotificationService.Worker;
using NotificationService.Worker.Consumers;
using NotificationService.Worker.Interfaces;
using NotificationService.Worker.Model;
using Refit;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.UseElasticsearchLogger(applicationName: "ecommerce-notification-worker")
	.CreateLogger();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


string? notificationServiceApiUrl = builder.Configuration.GetValue<string>("NotificationServiceApiUrl");
if (string.IsNullOrEmpty(notificationServiceApiUrl))
{
	throw new InvalidOperationException("NotificationServiceApiUrl is not configured properly.");
}

builder.Services.AddRefitClient<INotificationServiceApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(notificationServiceApiUrl));

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
if (rabbitMqSettings == null)
{
	throw new InvalidOperationException("RabbitMQ settings are not configured properly.");
}

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<NotificationConsumer>();


	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(rabbitMqSettings?.Host, rabbitMqSettings?.VirtualHost, h =>
		{
			h.Username(rabbitMqSettings?.Username ?? "");
			h.Password(rabbitMqSettings?.Username ?? "");
		});


		cfg.ReceiveEndpoint("notification_queue", e =>
		{
			e.ConfigureConsumer<NotificationConsumer>(context);

			// 5 defa dene ve 10 saniye bekle
			e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(10)));
		});
	});
});

var host = builder.Build();

try
{
	Log.Information("Uygulama baþlatýlýyor.");
	host.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Uygulama baþlatýlamadý!");
}
finally
{
	Log.CloseAndFlush();
}
