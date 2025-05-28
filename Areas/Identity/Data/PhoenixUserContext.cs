using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.Areas.Identity.Data;

namespace Phoenix.Areas.Identity.Data;

public class PhoenixUserContext : IdentityDbContext<IdentityUser>
{
    public PhoenixUserContext(DbContextOptions<PhoenixUserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
