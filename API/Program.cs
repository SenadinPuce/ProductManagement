using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductManagementContext>(options => options.UseSqlServer(

		builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
	var connString = builder.Configuration.GetConnectionString("Redis")
		?? throw new Exception("Cannot get redis connection string");
	var configuration = ConfigurationOptions.Parse(connString, true);
	return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IResponseCacheService, ResponseCacheService>();

var app = builder.Build();

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
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ProductManagementContext>();
	await context.Database.MigrateAsync();
	await ProductManagementContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

app.Run();
