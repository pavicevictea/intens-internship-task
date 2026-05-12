using HRPlatform.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Infrastructure
{
    public class PlatformDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skills> Skills { get; set; }

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>(candidate =>
            {
                candidate.HasIndex(c => c.Email).IsUnique();
                candidate.Property(c => c.FullName).IsRequired().HasMaxLength(150);
                candidate.Property(c => c.DateOfBirth).IsRequired();
                candidate.Property(c => c.ContactNumber).IsRequired().HasMaxLength(25);
                candidate.Property(c => c.Email).IsRequired().HasMaxLength(100);

            });

            modelBuilder.Entity<Skills>(skill =>
            {
                skill.HasIndex(s => s.Name).IsUnique();
                skill.Property(s => s.Name).IsRequired().HasMaxLength(150);
            });
        }
    }
}
