using System.Collections.Generic;
using RESTfulAPIDesign.Data.Conveters;
using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Repository.Generic;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class BookServiceImpl : IBookService
    {
        private IRepository<Book> repository;
        private readonly BookConverter converter;

        public BookServiceImpl(IRepository<Book> repository)
        {
            this.repository = repository;
            // Build the PersonConventer Instance
            this.converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return this.converter.ParseList(this.repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return this.converter.Parse(this.repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            // Converter the ValuesObjects to Entity
            var bookEntity = this.converter.Parse(book);
            bookEntity = this.repository.Create(bookEntity);
            // Converter the Entity to ValuesObjects
            return this.converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            // Converter the ValuesObjects to Entity
            var bookEntity = this.converter.Parse(book);
            bookEntity = this.repository.Update(bookEntity);
            // Converter the Entity to ValuesObjects
            return this.converter.Parse(bookEntity);
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
