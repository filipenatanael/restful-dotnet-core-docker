using System.Collections.Generic;
using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Services
{
    public interface IPersonService
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
