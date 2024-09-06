using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Model;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IOrderService, OrderService.Application.Services.OrderService>();

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
if (rabbitMqSettings == null)
{
	throw new InvalidOperationException("RabbitMQ settings are not configured properly.");
}

builder.Services.AddMassTransit(x =>
{

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(rabbitMqSettings?.Host, rabbitMqSettings?.VirtualHost, h =>
		{
			h.Username(rabbitMqSettings?.Username ?? "");
			h.Password(rabbitMqSettings?.Username ?? "");
		});
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
	var context = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();
	context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
