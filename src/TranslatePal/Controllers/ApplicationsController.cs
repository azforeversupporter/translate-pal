using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System.Threading.Tasks;
using TranslatePal.Data.SqlServer;

namespace TranslatePal.Controllers
{
    public class ApplicationsController : Controller
    {
        public ApplicationsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("/api/v1/applications/")]
        public async Task<IActionResult> Get()
        {
            var applications = await db.Applications
                .ToListAsync();

            return Ok(applications);
        }

        private ApplicationDbContext db;
    }
}
