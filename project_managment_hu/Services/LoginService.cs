using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public class LoginService : ILoginService
    {
        IConfiguration _config;
        UserContext _context;

        public LoginService(IConfiguration config, UserContext context)
        {
            _config = config;
            _context = context;


        }
        public string Generate(UserModel user)
        {
            UserModel u = _context.userModels.Include(i=>i.UserRoles).ThenInclude(UserRoles=>UserRoles.Role).FirstOrDefault(i=>i.Id==user.Id);
            List<Claim> claimList= new List<Claim>();
            if(user.UserRoles == null || user.UserRoles.Count==0)
            {
                return "No role added";
            }

            foreach(var role in u.UserRoles)
            {
                claimList.Add(new Claim("roles",role.Role.Name));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.NameIdentifier, user.FullName),
            //     new Claim(ClaimTypes.Email, user.EmailAddress),
            //     new Claim(ClaimTypes.GivenName, user.FirstName),
            //     new Claim(ClaimTypes.Surname, user.LastName),
            //   //  new Claim(ClaimTypes.Role, user.Role)
            // };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims:claimList,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string Login(UserLogin userLogin)
        {

            var user = _context.userModels.FirstOrDefault(u => u.EmailAddress.ToLower() == userLogin.EmailAddress.ToLower());
            if (user == null)
                return "No user";
            else if (PasswordVerify(user, userLogin))
            {
                return Generate(user);
            }
            else
            {
                return "Password is incorrect";
            }



        }



        ResponseModel ILoginService.Register(UserModelDto userModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {

                if (userExists(userModel.EmailAddress))
                    model.Messsage = "User Exists";
                UserModel user = new UserModel();
                user.FirstName = userModel.FirstName;
                user.EmailAddress = userModel.EmailAddress;
                user.FullName = userModel.FullName;
                user.LastName = userModel.LastName;
                user.Password = userModel.Password;
               // user.Role = userModel.Role;
                _context.Add(user);
                model.Messsage = "User Inserted Successfully";

                _context.SaveChanges();


                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }


        public bool PasswordVerify(UserModel userModel, UserLogin userLogin)
        {
            return userModel.Password == userLogin.Password;
        }
        public bool userExists(string EmailAddress)
        {
            if (_context.userModels.Any(u => u.EmailAddress.ToLower() == EmailAddress.ToLower()))
            {
                return true;
            }
            return false;
        }

    }


}