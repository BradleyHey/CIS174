using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobHunter.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed statuses
            builder.Entity<Status>().HasData(
                new Status { StatusId = "applied", Name = "Applied" },
                new Status { StatusId = "interviewing", Name = "Interviewing" },
                new Status { StatusId = "rejected", Name = "Rejected" },
                new Status { StatusId = "accepted", Name = "Accepted" }
            );
        }
    }
}
