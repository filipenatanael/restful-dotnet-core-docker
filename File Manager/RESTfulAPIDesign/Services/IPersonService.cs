using System.Collections.Generic;
using RESTfulAPIDesign.Data.ValuesObjects;
using Tapioca.HATEOAS.Utils;

namespace RESTfulAPIDesign.Services
{
    public interface IPersonService
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        List<PersonVO> FindByName(string fristName, string lastName);
        PersonVO Update(PersonVO person);
        void Delete(long id);
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
