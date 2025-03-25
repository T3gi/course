using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoenix.Data;
using Phoenix.Models;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PhoenixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhoenixContext") ?? throw new InvalidOperationException("Connection string 'PhoenixContext' not found.")));
builder.Services.AddDbContext<PhoenixUserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhoenixUserContextConnection") ?? throw new InvalidOperationException("Connection string 'PhoenixUserContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PhoenixUserContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
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
