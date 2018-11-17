using System;
using System.Collections.Generic;
using System.Linq;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Models.Context;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly MySQLContext context;

        public UserRepositoryImpl(MySQLContext context)
        {
            this.context = context;
        }

        public User FindByLogin(string login)
        {
            return this.context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}
