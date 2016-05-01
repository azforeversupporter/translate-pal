using Microsoft.Data.Entity;

namespace TranslatePal.Data.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Bundle> Bundles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bundle>()
                .HasAlternateKey(bundle => new { bundle.Name, bundle.ApplicationId });
        }
    }
}
