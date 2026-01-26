using System.ComponentModel.DataAnnotations;

namespace FirstResponsiveWebAppHey.Models
{
    public class FirstResponsiveWebAppModel
    {
        private const int MinYear = 1900;
        private const int MaxYear = 2026;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth year is required")]
        [Range(MinYear, MaxYear, ErrorMessage = "Enter a valid year")]
        public int YearOfBirth { get; set; }

        public int AgeThisYear()
        {
            return DateTime.Now.Year - YearOfBirth;
        }
    }
}