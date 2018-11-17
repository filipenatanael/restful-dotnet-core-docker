using System.Collections.Generic;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}
