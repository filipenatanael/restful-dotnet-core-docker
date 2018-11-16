using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPIDesign.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : Controller
    {
        private IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.bookService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = this.bookService.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(this.bookService.Create(book));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book book)
        {
            if (book == null) return BadRequest();
            var updateBook = this.bookService.Update(book);
            if (updateBook == null) return BadRequest();
            return new ObjectResult(updateBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.bookService.Delete(id);
            // Will return 204 status code
            return NoContent();
        }
    }
}
