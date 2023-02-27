using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class Issuses
    {
        [Key]
        public int IssueId { get; set; }
        public string IssueType { get; set; }
        public string IssueTitle { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public int projectsProjectId { get; set; }
        public Projects projects { get; set; }

        public int ReporterId { get; set; }
        public UserModel Reporter { get; set; }
        public int? AssigneeId { get; set; }
        public UserModel? Assignee { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        //public List<labels> labels { get; set; }
        public ICollection<IssueLabel> IssueLabels { get; set; }


        // public DateTime CreatedDate { get; set; }
        // public DateTime ModifiedDate { get; set; }

        // public Issuses()
        // {
        //     this.CreatedDate = DateTime.UtcNow;
        //     this.ModifiedDate = DateTime.UtcNow;
        // }








    }
}