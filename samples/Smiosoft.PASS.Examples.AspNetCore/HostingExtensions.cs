using System.Reflection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Smiosoft.PASS.Examples.AspNetCore
{
    internal static class HostingExtensions
    {
        public static Serilog.ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            builder.Services
                .AddPass(assemblies: Assembly.GetExecutingAssembly())
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "PASS Example API",
                        Version = "v1"
                    });
                })
                .AddRouting(options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                })
                .AddMvcCore()
                .AddApiExplorer()
                .AddDataAnnotations();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication application)
        {
            application.UseSerilogRequestLogging();
            application.UseRouting();
            application.UseSwagger();
            application.UseSwaggerUI();
            application.MapControllers();

            return application;
        }
    }
}
