using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPIDesign.Controllers
{
    public class BooksController : Controller
    {
        private IPersonService personService;

        public PersonsController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = this.personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(this.personService.Create(person));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book book)
        {
            if (person == null) return BadRequest();
            var updatePerson = this.personService.Update(person);
            if (updatePerson == null) return BadRequest();
            return new ObjectResult(updatePerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.personService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
