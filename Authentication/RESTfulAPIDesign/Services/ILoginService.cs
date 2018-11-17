using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services
{
    public interface ILoginService
    {
        object FindByLogin(UserVO user);
    }
}
