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

#pragma warning restore SA1200 // Using directives should be placed correctly

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services
    .AddControllersWithViews();

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

using (var scope = app.Services.CreateScope())
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    if (app.Environment.IsDevelopment())
    {
        await ApplicationDbContextSeed.SeedAsync(scope.ServiceProvider);
    }
}

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

app.Run();

public partial class Program
{
}