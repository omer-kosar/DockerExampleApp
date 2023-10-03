using ExampleApp.Models;
using ExampleApp.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });
builder.Services.AddControllersWithViews();

var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["DBPASSWORD"] ?? "mysecret";
builder.Services.AddDbContext<ProductDbContext>(options => options.UseMySql($"server={host};userid=root;pwd={password};port={port};database=products;SSL Mode=None", new MySqlServerVersion(new Version(8, 0, 29))));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<IRepository, ProductRepository>();
var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddEnvironmentVariables()
            .AddCommandLine(args).Build();
var app = builder.Build();

if ((config["INITDB"] ?? "false") == "true")
{
    Console.WriteLine("Preparing database...");
    SeedData.EnsurePopulated(app);
    Console.WriteLine("Preparation is completed...");
}
else
{
    Console.WriteLine("Starting ASP.NET");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.Run();
}
