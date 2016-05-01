using Microsoft.Data.Entity;

namespace TranslatePal.Data.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
