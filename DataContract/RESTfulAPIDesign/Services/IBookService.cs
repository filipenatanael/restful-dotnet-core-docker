using System.Collections.Generic;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services
{
    public interface IBookService
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
