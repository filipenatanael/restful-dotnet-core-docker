using System.Collections.Generic;
using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Services
{
    public interface IBookService
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
