using Microsoft.EntityFrameworkCore;

namespace TranslatePal.Data.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bundle>()
                .HasAlternateKey(bundle => new { bundle.Name, bundle.ApplicationId });

            modelBuilder.Entity<Element>()
                .HasAlternateKey(element => new { element.BundleId, element.ElementName });

            modelBuilder.Entity<Resource>()
                .HasAlternateKey(resource => new { resource.ElementId, resource.Language });
        }
    }
}
