using StockService.Worker;
using StockService.Worker.Interfaces;
using Refit;
using MassTransit;
using StockService.Worker.Consumers;
using StockService.Worker.Model;
using Serilog;
using ECommerce.Shared.Utilities.Loging.Serilog;


Log.Logger = new LoggerConfiguration()
	.UseElasticsearchLogger(applicationName: "ecommerce-stock-worker")
	.CreateLogger();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


string? stockServiceApiUrl = builder.Configuration.GetValue<string>("StockServiceApiUrl");
if (string.IsNullOrEmpty(stockServiceApiUrl))
{
	throw new InvalidOperationException("StockServiceApiUrl is not configured properly.");
}

builder.Services.AddRefitClient<IStockServiceApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(stockServiceApiUrl));


var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
if (rabbitMqSettings == null)
{
	throw new InvalidOperationException("RabbitMQ settings are not configured properly.");
}

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<StockUpdateConsumer>();
	x.AddConsumer<StockUpdateErrorConsumer>();

	x.UsingRabbitMq((context, cfg) =>
		{
			cfg.Host(rabbitMqSettings?.Host, rabbitMqSettings?.VirtualHost, h =>
			{
				h.Username(rabbitMqSettings?.Username ?? "");
				h.Password(rabbitMqSettings?.Username ?? "");
			});


			cfg.ReceiveEndpoint("stock_update_queue", e =>
			{
				e.ConfigureConsumer<StockUpdateConsumer>(context);

				// 5 defa dene ve 10 saniye bekle
				//e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5))); //Poly kullan�ld��� i�in gerek kalmad�

				// Hata kuyru�una iletilmesi i�in gerekli ayarlar
				e.UseInMemoryOutbox(context);

			});

			// Hata kuyru�u ayarlar�
			cfg.ReceiveEndpoint("stock_update_queue_error", e =>
			{
				e.ConfigureConsumer<StockUpdateErrorConsumer>(context);
			});
		});
});

builder.Logging.AddSerilog();

var host = builder.Build();


try
{
	Log.Information("Uygulama ba�lat�l�yor.");
	host.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Uygulama ba�lat�lamad�!");
}
finally
{
	Log.CloseAndFlush();
}


