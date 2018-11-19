using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tapioca.HATEOAS;

using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Services;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(this.personService.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person = this.personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        /* 
         * Query Params
         */
        [HttpGet("find-by-name")]
        [ProducesResponseType(typeof(PersonVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return new OkObjectResult(this.personService.FindByName(firstName, lastName));
        }

        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(PersonVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetPagedSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return new OkObjectResult(this.personService.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
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
        [ProducesResponseType(typeof(List<PersonVO>), 202)]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatePerson = this.personService.Update(person);
            if (updatePerson == null) return BadRequest();
            return new ObjectResult(updatePerson);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            this.personService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
