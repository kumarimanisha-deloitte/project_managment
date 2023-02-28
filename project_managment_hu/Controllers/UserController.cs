using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_managment_hu.Dto;
using project_managment_hu.Model;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

         IUserService _userservice;
        private readonly ILogger<UserController> _logger;


        public UserController(IUserService userservice,ILogger<UserController> logger)
        {
            _userservice= userservice;
            _logger = logger;


        }
       

        [HttpGet("Normal")]
        [Authorize(Roles = "Normal,Admin")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on normal user area");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    //FullName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    //EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    //FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    //LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    //UserRoles = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles="Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var employees = _userservice.GetUserList();
                if (employees == null) return NotFound();
                _logger.LogInformation("This is my log message all user list");
                return Ok(employees);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "This is my error log message with an exception.");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/id")]
        [Authorize(Roles = "Admin")]

        public List<UserModel> GetEmployeeByID(int id)
        {
            
            return _userservice.GetUserDetailsById(id);
            // return  emp;
        }


        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]

        public void DeleteEmployeeById(int id)
        {
            _userservice.DeleteEmployee(id);

        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult updateUserDetail(int userId, UserModelDto userModelDto)
        {
            try
            {
                var model = _userservice.UserDetailsUpdate(userId,userModelDto);
                _logger.LogInformation("This is my log message all update user");
                return Ok(model);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "This is my error log message with an exception.");
                return BadRequest();
            }

        }




    }
}