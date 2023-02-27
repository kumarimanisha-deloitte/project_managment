using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public class UserService : IUserService
    {
        UserContext _context;

         public UserService(UserContext context)
        {
        
            _context = context;


        }
        public List<UserModel> GetUserList()
        {
            List<UserModel> userList;
            try
            {
                userList = _context.userModels.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return userList;
        }

        public List<UserModel> GetUserDetailsById(int empId)
        {
            UserModel emp;
            List<UserModel> emp1;

            try
            {
                // emp = _context.Find < Employee > (empId);
                emp1 = _context.userModels.Include(s => s.projects).Include(s => s.Assignee).Include(s=>s.UserRoles).ThenInclude(UserRoles=>UserRoles.Role)
                .Where(s => s.Id == empId).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return emp1;
        }

        public void DeleteEmployee(int id)
        {
            UserModel emp = _context.Find<UserModel>(id);

            _context.Remove(emp);
            _context.SaveChanges();
        }

        public ResponseModel UserDetailsUpdate(int userId, UserModelDto userModelDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                UserModelDto _temp = _context.Find<UserModelDto>(userId);
                if (_temp != null)
                {
                    _temp.FirstName = userModelDto.FirstName;
                    _temp.EmailAddress = userModelDto.EmailAddress;
                    _temp.FullName = userModelDto.FullName;
                    _temp.LastName = userModelDto.LastName;
                    _temp.Password = userModelDto.Password;
                    //_temp.Role = userModelDto.Role;

                    _context.Update<UserModelDto>(_temp);
                    model.Messsage = "User Updated Successfully";
                    // }
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "User Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

    
    }
}