using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Model;

namespace project_managment_hu.Dto
{
    public class IssueDto
    {
        public int IssueId { get; set; }
        public int IssueType { get; set; }
        public string IssueTitle { get; set; }
        public string IssueDescription { get; set; }
        public int Status { get; set; }
        public int projectsProjectId { get; set; }

        public int ReporterId { get; set; }
        public int AssigneeId { get; set; }
       // public int projectId { get; set; }


        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}