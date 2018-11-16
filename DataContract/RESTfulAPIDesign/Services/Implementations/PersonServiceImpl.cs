using System.Collections.Generic;
using RESTfulAPIDesign.Data.Conveters;
using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Repository.Generic;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private IRepository<Person> repository;
        private readonly PersonConverter converter;

        public PersonServiceImpl(IRepository<Person> repository)
        {
            this.repository = repository;
            // Build the PersonConventer Instance
            this.converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return this.converter.ParseList(this.repository.FindAll());
        }

        public PersonVO FindById(long id)
        {   
            return this.converter.Parse(this.repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {   //          throw new Exception("Custom exception: ", exception);

         
            // Converter the ValuesObjects to Entity
            var personEntity = this.converter.Parse(person);
            personEntity = this.repository.Create(personEntity);
            // Converter the Entity to ValuesObjects
            return this.converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            // Converter the ValuesObjects to Entity
            var personEntity = this.converter.Parse(person);
            personEntity = this.repository.Update(personEntity);
            // Converter the Entity to ValuesObjects
            return this.converter.Parse(personEntity);
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
