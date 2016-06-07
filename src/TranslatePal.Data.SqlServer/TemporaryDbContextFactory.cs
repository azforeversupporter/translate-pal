using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TranslatePal.Data.SqlServer
{
    public class TemporaryDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=translatepal;Trusted_Connection=True;MultipleActiveResultSets=True");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
