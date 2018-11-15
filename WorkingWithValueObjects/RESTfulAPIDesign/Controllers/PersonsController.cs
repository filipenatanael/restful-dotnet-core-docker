using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Services;
using System;

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

        // GET api/version/controller/5
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.personService.FindAll());
        }

        // GET api/version/controller/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = this.personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/version/controller
        [HttpPost]
        public IActionResult Post([FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            try
            {
                return new ObjectResult(this.personService.Create(person));
            }
            catch (Exception exception)
            {
                return new ObjectResult(exception.InnerException.Message);
            }
           
        }

        // PUT api/version/controller/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatePerson = this.personService.Update(person);
            if (updatePerson == null) return BadRequest();
            return new ObjectResult(updatePerson);
        }

        // DELETE api/version/controller/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.personService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
