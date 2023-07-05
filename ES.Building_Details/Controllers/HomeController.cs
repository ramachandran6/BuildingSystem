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
        public readonly PersonContext personDbContext;

        public HomeController(PersonContext personDbContext)
        {
            this.personDbContext = personDbContext;
        }

        [HttpGet]
        [Route("/BuildingDetails")]

        public async Task<IActionResult> getBuildingDetails()
        {
            return Ok(await personDbContext.buildingSystems.ToListAsync());
        }

        [HttpPost]
        [Route("/BuildingDetails")]

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

                await personDbContext.buildingSystems.AddAsync(bs);
                await personDbContext.SaveChangesAsync();

                return Ok(bs);
            }
        }

        [HttpPut]
        [Route("/BuildingDetails/{id:guid}")]
        public async Task<IActionResult> updateBuildingDetails([FromRoute] Guid id,UpdateBuildingSystem ubs)
        {
            if(ubs == null)
            {
                return BadRequest("Enter some valid details");
            }
            else
            {
                var result = personDbContext.buildingSystems.FirstOrDefault(x => x.Id.Equals(id));
                if (result == null)
                {
                    return BadRequest("Building not found");
                }
                result.BuildingName = ubs.BuildingName;
                result.NoOfFloors = ubs.NoOfFloors;
                result.Generator_Staus = ubs.Generator_Staus;

                personDbContext.buildingSystems.Update(result);
                await personDbContext.SaveChangesAsync();

                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("/BuildingDetails/{id:guid}")]
        public async Task<IActionResult> deleteBuilding([FromRoute] Guid id) { 
        
            if(id == null)
            {
                return BadRequest("enter valid id");
            }
            else
            {
                var result = personDbContext.buildingSystems.FirstOrDefault(x => x.Id.Equals(id));
                if(result == null)
                {
                    return BadRequest("Building id not found");
                }

                personDbContext.buildingSystems.Remove(result);
                await personDbContext.SaveChangesAsync();

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
                var result = personDbContext.buildingSystems.FirstOrDefault(x => x.BuildingName.Equals(name));

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
                var result = personDbContext.buildingSystems.FirstOrDefault(x => x.BuildingName.Equals(name));

                if (result == null)
                {
                    return BadRequest("Building Name not found");
                }
                var status = result.Generator_Staus;

                return Ok(status);
            }

        }
        

        [HttpGet]
        [Route("/PersonDetails")]
        public async Task<IActionResult> getPersonDetails()
        {
            return Ok(await personDbContext.PersonDet.ToListAsync());
        }

        [HttpPost]
        [Route("/PersonDetails")]
        public async Task<IActionResult> addPersonDetails(InsertPersonRequest ipr)
        {
            if (ipr == null)
            {
                return BadRequest("Enter with some value");
            }
            else
            {
                PersonDatabase pd = new PersonDatabase();
                pd.personId = new Guid();
                pd.weight = ipr.weight;
                pd.fromFloor = ipr.fromFloor;
                pd.toFloor = ipr.toFloor;

                var result = await personDbContext.PersonDet.AddAsync(pd);
                await personDbContext.SaveChangesAsync();

                return Ok(result);
            }
        }

        [HttpPut]
        [Route("/PersonDetails/{id:guid}")]
        public async Task<IActionResult> updatePersonDetails([FromRoute] Guid id, UpdatePersonRequest upr)
        {
            if (upr == null)
            {
                return BadRequest("Enter some details");
            }
            else
            {
                var result = personDbContext.PersonDet.FirstOrDefault(x => x.personId.Equals(id));
                if (result == null)
                {
                    return BadRequest("The person not found");
                }
                result.weight = upr.weight == 0 ? result.weight : upr.weight;
                result.fromFloor = upr.fromFloor;
                result.toFloor = upr.toFloor;

                personDbContext.PersonDet.Update(result);
                await personDbContext.SaveChangesAsync();
                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("/PersonDetails/{id:guid}")]

        public async Task<IActionResult> deletePerson([FromRoute] Guid id)
        {
            if (id == null)
            {
                return BadRequest("enter  valid id");
            }
            else
            {
                var result = personDbContext.PersonDet.FirstOrDefault(x => x.personId.Equals(id));

                if (result == null)
                {
                    return BadRequest("personId not found");
                }

                personDbContext.PersonDet.Remove(result);
                await personDbContext.SaveChangesAsync();
                return Ok(result);

            }
        }

        [HttpGet]
        [Route("/getWeight")]
        public async Task<IActionResult> getPersonsWeight()
        {
            return Ok(await personDbContext.PersonDet.SumAsync(x => x.weight));
        }

        [HttpGet]
        [Route("/getPersons/{floor_num}")]
        public async Task<IActionResult> getPersons([FromRoute] byte floor_num)
        {
            if (float.IsNaN(floor_num))
            {
                return BadRequest("enter valid number");
            }
            else
            {
                return Ok(await personDbContext.PersonDet.FirstOrDefaultAsync(x => x.toFloor.Equals(floor_num)));
            }
        }

    }
}
