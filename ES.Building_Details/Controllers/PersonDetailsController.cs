using ES.BS.DataAccessLayer;
using ES.BS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.Building_Details.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PersonDetailsController : Controller
    {
        public readonly PersonContext personDbContext;

        public PersonDetailsController(PersonContext personDbContext)
        {
            this.personDbContext = personDbContext;
        }
        [HttpGet]
        [Route("Person")]
        public async Task<IActionResult> GetPersonDetails()
        {
            return Ok(await personDbContext.PersonDet.ToListAsync());
        }

        [HttpPost]
        [Route("/Person/{id:guid}")]
        public async Task<IActionResult> AddPersonDetails([FromRoute] Guid id,InsertPersonRequest ipr)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Enter valid id");
            }
            else
            {
                if(ipr == null)
                {
                    return BadRequest("Enter valid value");
                }
                var result = personDbContext.workerDetailss.FirstOrDefault(x => x.Id.Equals(id));
                if(result == null)
                {
                    return NotFound("Id not found");
                }
                PersonDatabase pd = new PersonDatabase();
                pd.personId = id;
                pd.weight = result.weight;
                //check with the floor range by adding building name in the person details
                pd.fromFloor = ipr.fromFloor;
                pd.toFloor = ipr.toFloor;

                await personDbContext.PersonDet.AddAsync(pd);
                await personDbContext.SaveChangesAsync();

                return Ok(pd);
            }
        }

        [HttpPut]
        [Route("/Person/{id:guid}")]
        public async Task<IActionResult> UpdatePersonDetails([FromRoute] Guid id, UpdatePersonRequest upr)
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
        [Route("/Person/{id:guid}")]

        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
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
        public async Task<IActionResult> GetPersonsWeight()
        {
            return Ok(await personDbContext.PersonDet.SumAsync(x => x.weight));
        }

        [HttpGet]
        [Route("/getPersons/{floor_num}")]
        public async Task<IActionResult> GetPersons([FromRoute] byte floor_num)
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
