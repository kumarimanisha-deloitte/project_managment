using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Model;

namespace project_managment_hu.Dto
{
    public class IssueDto
    {
        public int IssueId { get; set; }

        [Required(ErrorMessage = "The type is required")]
        public int IssueType { get; set; }
        
        [Required(ErrorMessage = "The title is required")]
        public string IssueTitle { get; set; }
        [Required(ErrorMessage = "The title is required")]
        public string IssueDescription { get; set; }

        [Required(ErrorMessage = "The status is required")]
        public int Status { get; set; }
        public int projectsProjectId { get; set; }

        public int ReporterId { get; set; }
        public int AssigneeId { get; set; }
       // public int projectId { get; set; }


        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}