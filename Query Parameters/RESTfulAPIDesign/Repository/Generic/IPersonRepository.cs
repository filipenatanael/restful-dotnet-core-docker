using RESTfulAPIDesign.Models;
using System.Collections.Generic;

namespace RESTfulAPIDesign.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string fristName, string lastName);
    }
}
