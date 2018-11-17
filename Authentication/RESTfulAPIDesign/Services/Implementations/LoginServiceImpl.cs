using RESTfulAPIDesign.Data.ValuesObjects;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Security.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class LoginServiceImpl : ILoginService
    {
        private IUserRepository repository;
        private SigningConfigurations signingConfigurations;
        private TokenConfiguration tokenConfiguration;

        public LoginServiceImpl(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            this.repository = repository;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfiguration = tokenConfiguration;
        } 

        public object FindByLogin(UserVO user)
        {
            bool credentialIsValid = false;

            if(user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = this.repository.FindByLogin(user.Login);
                credentialIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }

            if(credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                        }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(this.tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            } else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = this.tokenConfiguration.Issuer,
                Audience = this.tokenConfiguration.Audience,
                SigningCredentials = this.signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "OK"
            };
        }
    }
}
