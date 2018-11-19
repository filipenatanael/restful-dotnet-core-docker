using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Repository.Generic;
using System.Collections.Generic;

namespace RESTfulAPIDesign.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string fristName, string lastName);
    }
}
