using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Dto
{
    public class RoleDto
    {
    public  int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    }
}