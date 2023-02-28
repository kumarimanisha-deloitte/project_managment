using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Dto
{
    public class labelsDto
    {
        public int labelId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}