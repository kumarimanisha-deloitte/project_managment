using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project_managment_hu.Model
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        //  public string Role { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }


        [JsonIgnore]
        [InverseProperty("Reporter")]
        public virtual Issuses Reporter { get; set; }
        [JsonIgnore]
        [InverseProperty("Assignee")]
        public virtual ICollection<Issuses> Assignee { get; set; }
        public List<Projects> projects { get; set; }
        // public List<Issuses> AssignedIssues { get; set; }



    }
}