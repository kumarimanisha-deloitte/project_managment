using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }
        [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        public string project_description { get; set; }


        public int  project_createrId {get; set;}


        [JsonIgnore]
        public UserModel project_creater {get; set;}

        public List<Issuses> issuses { get; set; }


    }
}