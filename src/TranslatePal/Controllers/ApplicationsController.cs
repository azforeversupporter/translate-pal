using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var applications = await db.Apps
                .ToListAsync();

            return Ok(applications);
        }

        [HttpGet]
        [Route("/api/v1/applications/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var application = await db.Apps
                .SingleOrDefaultAsync(app => app.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpPost]
        [Route("/api/v1/applications/")]
        public async Task<IActionResult> Post([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Apps.Add(application);
            await db.SaveChangesAsync();

            return Created($"/api/v1/applications/{application.Id}", application);
        }
        
        [HttpDelete]
        [Route("/api/v1/applications/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await db.Apps
                .SingleOrDefaultAsync(app => app.Id == id);
                
            if (application == null) 
            {
                return NotFound();
            }
            
            db.Apps.Remove(application);
            await db.SaveChangesAsync();
            
            return Ok();
        }

        private ApplicationDbContext db;
    }
}
