using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface ILoginService
    {
        ResponseModel Register(UserModelDto userModel);

        public string Login(UserLogin userLogin);
        public string Generate(UserModel user);
        public bool PasswordVerify(UserModel userModel, UserLogin userLogin);
        public bool userExists(string EmailAddress);

       





    }
}