using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services
{
    public interface ILoginrService
    {
        User FindByLogin(string login);
    }
}
