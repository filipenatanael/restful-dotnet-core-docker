using System.Collections.Generic;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private IPersonRepository repository;

        public PersonServiceImpl(IPersonRepository repository)
        {
            this.repository = repository;
        }
        
        public Person Create(Person person)
        {   
            // Validation here...
            return this.repository.Create(person);
        }

        public List<Person> FindAll()
        {
            // Validation here...
            return this.repository.FindAll();
        }

        public Person FindById(long id)
        {
            return this.repository.FindById(id);
        }

        public Person Update(Person person)
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
