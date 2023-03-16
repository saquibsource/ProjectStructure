using Serilog;
using WebAPIApplication.Security;

namespace WebAPIApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();
                // Add services to the container.
                IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false).Build();

                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

                builder.Services.AddAppSettingsModule(configuration);
                builder.Services.AddSecurityModule();

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseSerilogRequestLogging();
                app.UseApplicationSecurity();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
            }
        }
    }
}