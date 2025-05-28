using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.Models;

namespace Phoenix.Areas.Identity.Data
{
    public class PhoenixContext : DbContext
    {
        public PhoenixContext (DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public DbSet<Phoenix.Models.Car> Car { get; set; } = default!;
    }
}
