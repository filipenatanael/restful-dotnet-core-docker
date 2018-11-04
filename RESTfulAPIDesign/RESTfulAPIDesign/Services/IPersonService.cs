using System.Collections.Generic;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
