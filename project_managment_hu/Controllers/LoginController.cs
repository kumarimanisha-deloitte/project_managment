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

        public LoginController(ILoginService loginservice)
        {
            _loginservice = loginservice;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult RegisterUser(UserModelDto userModel)
        {

            try
            {
                var model = _loginservice.Register(userModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
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


        [HttpGet("users")]
        // [Authorize(Roles="admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var employees = _loginservice.GetUserList();
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/id")]
        public List<UserModel> GetEmployeeByID(int id)
        {

            return _loginservice.GetUserDetailsById(id);
            // return  emp;
        }


        [HttpDelete]
        [Route("[action]")]
        public void DeleteEmployeeById(int id)
        {
            _loginservice.DeleteEmployee(id);

        }

         [HttpPut]
        [Route("[action]")]
        public IActionResult updateUserDetail(int userId, UserModelDto userModelDto)
        {
            try
            {
                var model = _loginservice.UserDetailsUpdate(userId,userModelDto);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


    }
}