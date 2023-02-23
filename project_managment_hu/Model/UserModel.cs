using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Role{ get; set; }
        public List<Projects> projects { get; set; }
        public List<Issuses> AssignedIssues { get; set; }



    }
}