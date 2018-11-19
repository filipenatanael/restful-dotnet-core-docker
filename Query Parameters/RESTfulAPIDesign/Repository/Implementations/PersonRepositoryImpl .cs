using System.Collections.Generic;
using System.Linq;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Models.Context;
using RESTfulAPIDesign.Repository;
using RESTfulAPIDesign.Repository.Generic;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonRepositoryImpl : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepositoryImpl(MySQLContext context) : base(context) { }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return this.context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return this.context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return this.context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            return this.context.Persons.ToList();
        }
    }
}
