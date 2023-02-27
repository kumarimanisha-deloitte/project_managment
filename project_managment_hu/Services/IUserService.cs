using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface IUserService
    {
       // UserModel GetCurrentUser();s
        public List<UserModel> GetUserList();
        public List<UserModel> GetUserDetailsById(int empId);

        void DeleteEmployee(int id);
        ResponseModel UserDetailsUpdate(int id, UserModelDto userModelDto);


        
    }
}