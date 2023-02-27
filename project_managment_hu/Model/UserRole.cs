using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class UserRole
    {
    public int UserId { get; set; }
    public UserModel User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    }
}