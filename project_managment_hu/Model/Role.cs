using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class Role
    {
    public  int Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    }
}