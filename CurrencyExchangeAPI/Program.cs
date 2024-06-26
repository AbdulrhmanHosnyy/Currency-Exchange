
using CurrencyExchangeAPI.Handlers;
using CurrencyExchangeAPI.Helpers;
using Microsoft.OpenApi.Models;

namespace CurrencyExchangeAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Currency Exchange API",
					Version = "v1",
					Description = "API for currency exchange rates",
					Contact = new OpenApiContact
					{
						Name = "Abdulrhman Hosny",
						Email = "abdulrhmanhosnymuhammed@gmail.com",
					}
				});
			});

			// Register the configuration settings
			builder.Services.Configure<CurrencyExchangeSettings>(builder.Configuration.GetSection("CurrencyExchangeSettings"));

			// Add memory caching
			builder.Services.AddMemoryCache();

			//	Register Logging Services
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			#region Dependency Injection
			builder.Services.AddTransient<CurrencyExchangeQueryHandler>();
			#endregion


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Exchange API V1");
				});
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
