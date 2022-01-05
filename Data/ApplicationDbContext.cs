using Microsoft.EntityFrameworkCore;
using SpotOps.Api.Models;

namespace SpotOps.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Spot> Spots { get; set; } = null!;
    }
}
