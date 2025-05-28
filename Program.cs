using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoenix.Areas.Identity.Data;
using Phoenix.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PhoenixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhoenixContext") ?? throw new InvalidOperationException("Connection string 'PhoenixContext' not found.")));
builder.Services.AddDbContext<PhoenixUserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhoenixUserContextConnection") ?? throw new InvalidOperationException("Connection string 'PhoenixUserContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PhoenixUserContext>();

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.AccessDeniedPath = "/Home";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Dealer", policy =>
          policy.RequireRole(["Admin", "Ford"]));
});

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
