using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        ILoginService _loginservice;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginservice,ILogger<LoginController> logger)
        {
            _loginservice = loginservice;
            _logger=logger;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult RegisterUser(UserModelDto userModel)
        {

            try
            {
                if (ModelState.IsValid){
                var model = _loginservice.Register(userModel);
                return Ok(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
               // return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
                _logger.LogError(e, "This is my error log message with an exception.");

            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]

        public string Login([FromBody] UserLogin userLogin)
        {
            string token = _loginservice.Login(userLogin);
            return token;
        }




    }
}