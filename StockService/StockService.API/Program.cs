using ECommerce.Shared.Utilities.Loging.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StockService.Application.Interfaces;
using StockService.Infrastructure.Data;


Log.Logger = new LoggerConfiguration()
	.UseElasticsearchLogger(applicationName:"ecommerce-stock-api") 
	.CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<StockContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStockService, StockService.Application.Services.StockService>();


builder.Host.UseSerilog();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{

	// SeedData sýnýfýný çaðýrarak seed verilerini ekler
	SeedData.Initialize(serviceScope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


try
{
	Log.Information("Uygulama baþlatýlýyor.");
	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Uygulama baþlatýlamadý!");
}
finally
{
	Log.CloseAndFlush();
}

