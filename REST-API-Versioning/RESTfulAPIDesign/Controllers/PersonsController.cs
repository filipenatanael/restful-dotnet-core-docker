using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Services;

namespace RESTfulAPIDesign.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController : Controller
    {
        private IPersonService personService;

        public PersonsController(IPersonService personService)
        {
            this.personService = personService;
        }

        // GET api/persons
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.personService.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = this.personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/persons
        [HttpPost]
        public IActionResult Post([FromBody]Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(this.personService.Create(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(this.personService.Update(person));
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.personService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
