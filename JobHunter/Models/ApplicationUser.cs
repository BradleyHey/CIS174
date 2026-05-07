using Microsoft.AspNetCore.Identity;

namespace JobHunter.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
