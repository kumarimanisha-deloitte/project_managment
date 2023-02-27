using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public class RoleService : IRoleService
    {
          UserContext _contest;
        public RoleService(UserContext contest)
        {
            _contest = contest;
        }

        public ResponseModel AddRoleToUser(int RoleId, int UserId)
        {
             ResponseModel model = new ResponseModel();
            try
            {
                var user = _contest.userModels.FirstOrDefault(u => u.Id ==UserId);
                var role = _contest.roles.FirstOrDefault(u => u.Id == RoleId);



                if (user != null && role != null)
                {

                    var UserRole = new UserRole
                    {
                        Role = role,
                        User = user
                    };

                    _contest.userRoles.Add(UserRole);
                    model.Messsage = "Role Added Successfully";

                    _contest.SaveChanges();
                    model.IsSuccess = true;


                }
                else if (user == null)
                {
                    model.Messsage = ($"User with ID  does not exist.");
                }
                else
                {
                    model.Messsage = ($"Role with ID  does not exist.");

                }
                          }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel CreateRole(RoleDto roleDto)
        {
             ResponseModel model = new ResponseModel();
            try
            {

                Role role = new Role();
                role.Name = roleDto.Name;
                _contest.Add<Role>(role);
                model.Messsage = "Role Added Successfully";
                // }
                _contest.SaveChanges();
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public List<Role> GetAllRoles()
        {
             List<Role> roleList;
            try
            {
                roleList = _contest.roles.Include(u=>u.UserRoles).ToList();
               // .ThenInclude(issueLabel => issueLabel.Label)
                //ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return roleList;
        }
    }
}