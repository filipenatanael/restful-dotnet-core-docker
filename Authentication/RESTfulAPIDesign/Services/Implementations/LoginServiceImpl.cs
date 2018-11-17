using RESTfulAPIDesign.Models;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class LoginServiceImpl : IUserService
    {
        private IUserRepository repository;

        public LoginServiceImpl(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User FindByLogin(string login)
        {
            throw new System.NotImplementedException();
        }
    }
}
