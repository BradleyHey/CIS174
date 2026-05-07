using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;

namespace JobHunter.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [JsonIgnore]
        public User? User { get; set; }

        [Required(ErrorMessage = "Please enter a company name.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a job title.")]
        public string JobTitle { get; set; } = string.Empty;

        public string? Location { get; set; }

        [Required(ErrorMessage = "Please enter an application date.")]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please select a status.")]
        public string StatusId { get; set; } = string.Empty;
        public Status? Status { get; set; }

        public string? RecruiterInformation { get; set; }
        public string? InterviewDates { get; set; }
        public string? Notes { get; set; }
    }
}
