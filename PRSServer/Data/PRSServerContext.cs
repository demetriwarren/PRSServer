using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRSServer.Models;

namespace PRSServer.Data
{
    public class PRSDbContext : DbContext
    {
        public PRSDbContext (DbContextOptions<PRSDbContext> options)
            : base(options)
        {
        }

        public DbSet<PRSServer.Models.User> User { get; set; } = default!;
        public DbSet<PRSServer.Models.Vendor> Vendor { get; set; } = default!;
        public DbSet<PRSServer.Models.Product> Product { get; set; } = default!;
        public DbSet<PRSServer.Models.Request> Request { get; set; } = default!;
        public DbSet<PRSServer.Models.RequestLine> RequestLine { get; set; } = default!;
    }
}
