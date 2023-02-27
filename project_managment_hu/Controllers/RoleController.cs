using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_managment_hu.Dto;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [ApiController]
    [Route("api/[controller]")]
    public class RoleController:ControllerBase
    {
          IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService= roleService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateRole(RoleDto roleDto)
        {
            try
            {
                var model = _roleService.CreateRole(roleDto);
                return Ok(model);
           }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles="Admin")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var labels = _roleService.GetAllRoles();
                if (labels == null) return NotFound();
                return Ok(labels);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

         [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]

        public IActionResult AddRoleToUser(int RoleId, int UserId)
        {
            try
            {
                var model = _roleService.AddRoleToUser(RoleId, RoleId);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}