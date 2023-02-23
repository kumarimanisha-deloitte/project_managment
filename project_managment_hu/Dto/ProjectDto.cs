using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_managment_hu.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string project_description { get; set; }

        public int project_createrId {get; set;}

        
    }
}