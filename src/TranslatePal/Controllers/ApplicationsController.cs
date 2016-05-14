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

        [HttpGet]
        [Route("/api/v1/applications/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var application = await db.Applications
                .Include(a => a.Bundles)
                .SingleOrDefaultAsync(app => app.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            return Ok(application);
        }

        [HttpPost]
        [Route("/api/v1/applications/")]
        public async Task<IActionResult> Post([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            db.Applications.Add(application);
            await db.SaveChangesAsync();

            return Created($"/api/v1/applications/{application.Id}", application);
        }
        
        [HttpDelete]
        [Route("/api/v1/applications/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await db.Applications
                .SingleOrDefaultAsync(app => app.Id == id);
                
            if (application == null) 
            {
                return HttpNotFound();
            }
            
            db.Applications.Remove(application);
            await db.SaveChangesAsync();
            
            return Ok();
        }

        private ApplicationDbContext db;
    }
}
