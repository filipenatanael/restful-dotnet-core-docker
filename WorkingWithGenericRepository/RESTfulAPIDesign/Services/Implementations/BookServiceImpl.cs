using System.Collections.Generic;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class BookServiceImpl : IBookService
    {
        private IBookRepository repository;

        public BookServiceImpl(IPersonRepository repository)
        {
            this.repository = repository;
        }
        
        public Book Create(Book book)
        {   
            // Validation here...
            return this.repository.Create(book);
        }

        public List<Book> FindAll()
        {
            // Validation here...
            return this.repository.FindAll();
        }

        public Book FindById(long id)
        {
            return this.repository.FindById(id);
        }

        public Book Update(Book person)
        {
            return this.repository.Update(person);
        }

        public void Delete(long id)
        {
            this.repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return this.repository.Exists(id);
        }
    }
}
