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

        public UserController(IUserService userservice)
        {
            _userservice= userservice;

        }
        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FullName}, you are an {currentUser.Role}");
        }


        [HttpGet("Proect_Manager")]
        [Authorize(Roles = "Proect_Manager")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FullName}, you are a {currentUser.Role}");
        }

        [HttpGet("Normal")]
        [Authorize(Roles = "Normal")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FullName}, you are an {currentUser.Role}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    FullName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
        [HttpGet("users")]
        // [Authorize(Roles="admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var employees = _userservice.GetUserList();
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

            return _userservice.GetUserDetailsById(id);
            // return  emp;
        }


        [HttpDelete]
        [Route("[action]")]
        public void DeleteEmployeeById(int id)
        {
            _userservice.DeleteEmployee(id);

        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult updateUserDetail(int userId, UserModelDto userModelDto)
        {
            try
            {
                var model = _userservice.UserDetailsUpdate(userId,userModelDto);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }




    }
}