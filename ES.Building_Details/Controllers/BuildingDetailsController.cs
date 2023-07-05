using ES.BS.DataAccessLayer;
using ES.BS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.Building_Details.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class BuildingDetailsController : Controller
    {
        public readonly PersonContext personDbContext;

        public BuildingDetailsController(PersonContext personDbContext)
        {
            this.personDbContext = personDbContext;
        }

        [HttpGet]
        [Route("/Building")]
        public async Task<IActionResult> GetBuildingDetails()
        {
            return Ok(await personDbContext.BuildingSystemss.ToListAsync());
        }

        [HttpPost]
        [Route("/Building")]
        public async Task<IActionResult> AddBuildingDetails(InsertBuildingSystem ibs)
        {
            if (ibs == null) {
                return BadRequest("enter valid details");
            }
            else
            {
                BuildingSystem bs = new BuildingSystem();

                bs.Id = new Guid();
                bs.BuildingName = ibs.BuildingName;
                bs.NoOfFloors = ibs.NoOfFloors;
                bs.Generator_Staus = ibs.Generator_Staus;

                await personDbContext.BuildingSystemss.AddAsync(bs);
                await personDbContext.SaveChangesAsync();

                return Ok(bs);
            }
        }

        [HttpPut]
        [Route("/Building/{id:guid}")]
        public async Task<IActionResult> UpdateBuildingDetails([FromRoute] Guid id,UpdateBuildingSystem ubs)
        {
            if(ubs == null)
            {
                return BadRequest("Enter some valid details");
            }
            else
            {
                var result = personDbContext.BuildingSystemss.FirstOrDefault(x => x.Id.Equals(id));
                if (result == null)
                {
                    return BadRequest("Building not found");
                }
                result.BuildingName = ubs.BuildingName;
                result.NoOfFloors = ubs.NoOfFloors;
                result.Generator_Staus = ubs.Generator_Staus;

                personDbContext.BuildingSystemss.Update(result);
                await personDbContext.SaveChangesAsync();

                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("/Building/{id:guid}")]
        public async Task<IActionResult> DeleteBuilding([FromRoute] Guid id) { 
        
            if(id == null)
            {
                return BadRequest("enter valid id");
            }
            else
            {
                var result = personDbContext.BuildingSystemss.FirstOrDefault(x => x.Id.Equals(id));
                if(result == null)
                {
                    return BadRequest("Building id not found");
                }

                personDbContext.BuildingSystemss.Remove(result);
                await personDbContext.SaveChangesAsync();

                return Ok(result);

            }
        }

        [HttpGet]
        [Route("/getNumberOfFloors/{name}")]
         
        public async Task<IActionResult> GetNumberOfFloors([FromRoute] string name)
        {
            if(name == null)
            {
                return BadRequest("enter the building name");
            }
            else
            {
                var result = personDbContext.BuildingSystemss.FirstOrDefault(x => x.BuildingName.Equals(name));

                if(result == null)
                {
                    return BadRequest("Building Name not found");
                }
                var num = result.NoOfFloors;

                return Ok(num);
            }

        }

        [HttpGet]
        [Route("/getGeneratorStatus/{name}")]

        public async Task<IActionResult> GetGeneratorStatus([FromRoute] string name)
        {
            if (name == null)
            {
                return BadRequest("enter the building name");
            }
            else
            {
                var result = personDbContext.BuildingSystemss.FirstOrDefault(x => x.BuildingName.Equals(name));

                if (result == null)
                {
                    return BadRequest("Building Name not found");
                }
                var status = result.Generator_Staus;

                return Ok(status);
            }

        }

    }
}
