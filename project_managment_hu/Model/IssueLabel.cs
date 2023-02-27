using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class IssueLabel
    {
    public int IssueId { get; set; }
    public Issuses Issue { get; set; }

    public int LabelId { get; set; }
   // [JsonIgnore]
    public labels Label { get; set; }
    }
}