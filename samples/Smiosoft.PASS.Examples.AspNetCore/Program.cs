using Serilog;
using Smiosoft.PASS.Examples.AspNetCore;

Log.Logger = HostingExtensions.CreateSerilogLogger();

try
{
    Log.Information("Starting up...");
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureServices();

    var application = builder.Build();
    application.ConfigurePipeline();
    application.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly.");
}
finally
{
    Log.Information("Shut down complete.");
    Log.CloseAndFlush();
}

