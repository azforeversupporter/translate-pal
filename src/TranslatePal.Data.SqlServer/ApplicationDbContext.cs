using Microsoft.EntityFrameworkCore;
using OpenIddict;

namespace TranslatePal.Data.SqlServer
{
    public class ApplicationDbContext : OpenIddictDbContext<ApplicationUser, ApplicationRole>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Apps { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .Property(typeof(string), "languages")
                .HasColumnName("Languages")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Element>()
                .HasAlternateKey(e => new { e.ApplicationId, e.Name });

            modelBuilder.Entity<Resource>()
                .HasAlternateKey(r => new { r.ElementId, r.Language });
        }
    }
}
