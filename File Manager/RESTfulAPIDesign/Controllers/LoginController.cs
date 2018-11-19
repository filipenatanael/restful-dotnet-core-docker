using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Services;
using Microsoft.AspNetCore.Authorization;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : Controller
    {
        private ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]UserVO user)
        {
            if (user == null) return BadRequest();
            return this.loginService.FindByLogin(user);
        }
    }
}
