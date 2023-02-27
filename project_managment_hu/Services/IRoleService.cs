using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface IRoleService
    {
         ResponseModel CreateRole(RoleDto roleDto);

         public List<Role> GetAllRoles();

        ResponseModel AddRoleToUser(int RoleId, int UserId);

    }
}