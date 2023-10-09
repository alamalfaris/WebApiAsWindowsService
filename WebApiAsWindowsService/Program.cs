using Microsoft.Extensions.Hosting.WindowsServices;

namespace WebApiAsWindowsService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webOptions = new WebApplicationOptions
			{
				Args = args,
				ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
			};
			var builder = WebApplication.CreateBuilder(webOptions);
			builder.Host.UseWindowsService();

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}