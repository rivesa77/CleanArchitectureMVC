// <copyright file="Program.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

#pragma warning disable SA1200 // Using directives should be placed correctly

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Ricardo.CleanArchitectureMVC.Application.Extensions;
using Ricardo.CleanArchitectureMVC.Infrastructure.Data;
using Ricardo.CleanArchitectureMVC.Infrastructure.Extensions;
using Serilog;
using Serilog.Sinks.MSSqlServer;

#pragma warning restore SA1200 // Using directives should be placed correctly

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

string applicationStage = "host creation";

try
{
    Log.Information("Starting CleanArchitectureMVC");

    var builder = WebApplication.CreateBuilder(args);

    applicationStage = "service configuration";

    // Add services to the container.
    string connectionString = builder
        .Configuration
        .GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services
        .AddControllersWithViews();

    ConfigureSerilogServices(builder, connectionString);

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

        options.DefaultRequestCulture = new RequestCulture(englishCulture);
        options.SupportedCultures = [englishCulture];
        options.SupportedUICultures = [englishCulture];
        options.ApplyCurrentCultureToResponseHeaders = true;
    });

    builder.Services
        .AddApplicationServiceCollection()
        .AddInfrastructureServiceCollection(connectionString);

    var app = builder.Build();

    RegisterApplicationLifetimeLogging(app);

    applicationStage = "database migration and seed";
    await InitializeDatabaseAsync(app);

    applicationStage = "request pipeline configuration";

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseRequestLocalization();

    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();

    applicationStage = "application runtime";
    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(
        exception,
        "CleanArchitectureMVC terminated unexpectedly during {ApplicationStage}",
        applicationStage);

    throw;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{
    private static void ConfigureSerilogServices(WebApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddSerilog((services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = false,
                        BatchPostingLimit = 50,
                        BatchPeriod = TimeSpan.FromSeconds(5),
                    });
        });
    }

    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();

        ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        logger.LogInformation("Applying pending database migrations");
        await context.Database.MigrateAsync().ConfigureAwait(false);
        logger.LogInformation("Database migrations completed");

        if (app.Environment.IsDevelopment())
        {
            logger.LogInformation("Starting development database seed");
            await ApplicationDbContextSeed.SeedAsync(scope.ServiceProvider).ConfigureAwait(false);
            logger.LogInformation("Development database seed completed");
        }
    }

    private static void RegisterApplicationLifetimeLogging(WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
            Log.Information(
                "CleanArchitectureMVC started in {EnvironmentName}",
                app.Environment.EnvironmentName));

        app.Lifetime.ApplicationStopping.Register(() =>
            Log.Information("CleanArchitectureMVC is stopping"));

        app.Lifetime.ApplicationStopped.Register(() =>
            Log.Information("CleanArchitectureMVC stopped"));
    }
}
