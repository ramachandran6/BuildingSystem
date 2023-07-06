using ES.BS.DataAccessLayer;
using ES.BS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.Building_Details.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WorkerDetailsController : Controller
    {
        public readonly PersonContext personDbContext;

        public WorkerDetailsController(PersonContext personDbContext)
        {
            this.personDbContext = personDbContext;
        }
        [HttpGet]
        [Route("/Worker")]
        public async Task<IActionResult> GetWorkerDetails()
        {
            return Ok(await personDbContext.workerDetailss.ToListAsync());
        }

        [HttpPost]
        [Route("/Worker")]
        public async Task<IActionResult> AddWorkerDetails(InsertWorkerRequest iwr)
        {
            if(iwr == null)
            {
                return BadRequest("Enter valid Details");
            }
            else
            {
                WorkerDetails wd = new WorkerDetails();
                wd.Id = new Guid();
                wd.weight = iwr.weight;

                var res = await personDbContext.BuildingSystemss.FirstOrDefaultAsync(x => x.BuildingName.Equals(iwr.BuildingName));

                if(res == null)
                {
                    return BadRequest("Building Name not found");
                }
                wd.BuildingName = iwr.BuildingName;
                await personDbContext.workerDetailss.AddAsync(wd);
                await personDbContext.SaveChangesAsync();
                return Ok(wd);
            }
        }

        [HttpPut]
        [Route("/Worker/{id:guid}")]
        public async Task<IActionResult> UpdateWorkerDetails([FromRoute] Guid id,UpdateWorkerRequest uwr)
        {
            if(id == Guid.Empty)
            {
                return BadRequest("enter id");
            }
            else
            {
                var res = personDbContext.workerDetailss.FirstOrDefault(x=> x.Id == id);
                if(res == null)
                {
                    return NotFound("Id not found");
                }

                if(uwr == null)
                {
                    return BadRequest("enter valid details");
                }
                res.weight = uwr.weight;
                res.BuildingName = uwr.BuildingName;
                personDbContext.workerDetailss.Update(res);
                await personDbContext.SaveChangesAsync();

                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("/Worker/{id:guid}")]
        public async Task<IActionResult> DeleteWorkerDetails([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("enter id");
            }
            else
            {
                var res = personDbContext.workerDetailss.FirstOrDefault(x => x.Id == id);
                if (res == null)
                {
                    return NotFound("Id not found");
                }
                
                personDbContext.workerDetailss.Remove(res);
                await personDbContext.SaveChangesAsync();

                return Ok(res);
            }
        }

    }
}
