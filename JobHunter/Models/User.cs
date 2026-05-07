using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace JobHunter.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
        
        [NotMapped]
        public IList<string> RoleNames { get; set; } = null!;
    }
}
