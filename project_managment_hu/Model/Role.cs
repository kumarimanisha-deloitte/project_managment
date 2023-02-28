using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class Role
    {
    public  int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    }
}