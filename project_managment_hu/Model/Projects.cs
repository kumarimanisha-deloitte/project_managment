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
        public string project_description { get; set; }


        public int  project_createrId {get; set;}


        [JsonIgnore]
        public UserModel project_creater {get; set;}

        public List<Issuses> issuses { get; set; }


    }
}