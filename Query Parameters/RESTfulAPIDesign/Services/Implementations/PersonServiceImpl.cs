using System.Collections.Generic;
using RESTfulAPIDesign.Data.Conveters;
using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Repository.Generic;
using RESTfulAPIDesign.Repository;
using Tapioca.HATEOAS.Utils;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private IPersonRepository repository;
        private readonly PersonConverter converter;

        public PersonServiceImpl(IPersonRepository repository)
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
        {
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return this.converter.ParseList(this.repository.FindByName(firstName, lastName));
        }

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = @"select * from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.firstName like '%{name}%'";

            query = query + $" order by p.firstName {sortDirection} limit {pageSize} offset {page}";

            string countQuery = @"select count(*) from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.firstName like '%{name}%'";

            var persons = this.repository.FindWithPagedSearch(query);

            int totalResults = this.repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = this.converter.ParseList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }
}
