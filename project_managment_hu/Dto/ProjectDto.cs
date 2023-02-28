using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Dto
{
    public class ProjectDto
    {

        public int ProjectId { get; set; }
        [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        public string project_description { get; set; }

        public int project_createrId {get; set;}

        
    }
}