using IG.ENMS.Starlink.Common;
using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.File;
using Serilog.AspNetCore;
using Serilog.Sinks;
using StarlinkDC.Hubs;
using System.Diagnostics;

namespace IG.ENMS.Starlink;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		//Read the config file.
		var config = builder.Configuration;
		config.SetBasePath((Debugger.IsAttached)
			? AppDomain.CurrentDomain.BaseDirectory
			: "C:\\Program Files\\InmarsatGov_apps\\ig-enms-starlink");
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
		config.AddEnvironmentVariables();

		AppSettings appSettings = new AppSettings();
		config.GetSection("AppSettings").Bind(appSettings);

		Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
			.WriteTo.File(appSettings.AppLogging.FileLocation, rollingInterval: RollingInterval.Hour)
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddWindowsService(options => {
            options.ServiceName = "ig_enms_starlink Service";
        });

        builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IgenmsContext") ?? throw new InvalidOperationException("Connection string 'IgenmsContext' not found.")));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IgcmsContext") ?? throw new InvalidOperationException("Connection string 'IgcmsContext' not found.")));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //Add services to the container.
        builder.Services.AddSignalR();
		builder.Services.AddMemoryCache();
		builder.Services.AddControllers(options =>
		{
			options.EnableEndpointRouting = false; // Disable default endpoint routing
		});
        builder.Services.AddResponseCaching();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

		builder.Services.AddSingleton(appSettings);
		builder.Services.AddSingleton<StarlinkSM>();
        builder.Services.AddSingleton<AppLog>();

        builder.Services.AddHostedService<Services.APITokenService>();
        builder.Services.AddHostedService<Services.Pollers.ServiceData>();
        builder.Services.AddHostedService<Services.Pollers.TelemetryData>();
        builder.Services.AddHostedService<Services.Pollers.UsageData>();
        builder.Services.AddHostedService<Services.StateMachinePersistence>();
        builder.Services.AddHostedService<Services.JsonProcessorService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DbContext>();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapHub<SystemStatusHub>("/systemstatushub");
        app.MapControllers();

        app.Run();
    }
}