using System.ComponentModel.DataAnnotations;
namespace ContactApp.Models;

public class Contact
{
    public int ContactId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;
}