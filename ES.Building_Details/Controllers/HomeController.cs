using ES.BS.DataAccessLayer;
using ES.BS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.Building_Details.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class HomeController : Controller
    {
        public readonly BuildingDbContext buildingDbContext;
        public HomeController(BuildingDbContext buildingDbContext)
        {
            this.buildingDbContext = buildingDbContext;
        }
        
        [HttpGet]

        public async Task<IActionResult> getBuildingDetails()
        {
            return Ok(await buildingDbContext.buildingSystems.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> addBuildingDetails(InsertBuildingSystem ibs)
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

                await buildingDbContext.buildingSystems.AddAsync(bs);
                await buildingDbContext.SaveChangesAsync();

                return Ok(bs);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateBuildingDetails([FromRoute] Guid id,UpdateBuildingSystem ubs)
        {
            if(ubs == null)
            {
                return BadRequest("Enter some valid details");
            }
            else
            {
                var result = buildingDbContext.buildingSystems.FirstOrDefault(x => x.Id.Equals(id));
                if (result == null)
                {
                    return BadRequest("Building not found");
                }
                result.BuildingName = ubs.BuildingName;
                result.NoOfFloors = ubs.NoOfFloors;
                result.Generator_Staus = ubs.Generator_Staus;

                buildingDbContext.buildingSystems.Update(result);
                await buildingDbContext.SaveChangesAsync();

                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteBuilding([FromRoute] Guid id) { 
        
            if(id == null)
            {
                return BadRequest("enter valid id");
            }
            else
            {
                var result = buildingDbContext.buildingSystems.FirstOrDefault(x => x.Id.Equals(id));
                if(result == null)
                {
                    return BadRequest("Building id not found");
                }

                buildingDbContext.buildingSystems.Remove(result);
                await buildingDbContext.SaveChangesAsync();

                return Ok(result);

            }
        }

        [HttpGet]
        [Route("/getNumberOfFloors/{name}")]
         
        public async Task<IActionResult> getNumberOfFloors([FromRoute] string name)
        {
            if(name == null)
            {
                return BadRequest("enter the building name");
            }
            else
            {
                var result = buildingDbContext.buildingSystems.FirstOrDefault(x => x.BuildingName.Equals(name));

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

        public async Task<IActionResult> getGeneratorStatus([FromRoute] string name)
        {
            if (name == null)
            {
                return BadRequest("enter the building name");
            }
            else
            {
                var result = buildingDbContext.buildingSystems.FirstOrDefault(x => x.BuildingName.Equals(name));

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
