using HRPlatform.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Infrastructure
{
    public class PlatformDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skills> Skills { get; set; }

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }
    }
}
