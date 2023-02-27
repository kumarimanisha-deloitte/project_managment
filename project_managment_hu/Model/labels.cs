using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class labels
    {
            [Key]
            public int labelId { get; set; }
            public string Name { get; set; }

            // public List<Issuses> issuses { get; set; }
           // [JsonIgnore]
            public ICollection<IssueLabel> IssueLabels { get; set; }


            

    }
}