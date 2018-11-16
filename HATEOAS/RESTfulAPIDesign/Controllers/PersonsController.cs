using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Services;
using System;
using Tapioca.HATEOAS;

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
        // GET api/version/controller
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(this.personService.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person = this.personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
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

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatePerson = this.personService.Update(person);
            if (updatePerson == null) return BadRequest();
            return new ObjectResult(updatePerson);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            this.personService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
