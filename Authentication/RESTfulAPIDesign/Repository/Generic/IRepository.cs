using RESTfulAPIDesign.Models.Base;
using System.Collections.Generic;

namespace RESTfulAPIDesign.Repository.Generic
{
    /* FILE: PersonServiceImpl.cs
     * public interface IRepository<Person> where Person : BaseEntity 
     */
    public interface IRepository<T> where T : BaseEntity
    {
        /* T(TEntity) will be define in runtime by c# */
        T Create(T entity);
        T FindById(long id);
        List<T> FindAll();
        T Update(T entity);
        void Delete(long id);

        bool Exists(long? id);
    }
}
