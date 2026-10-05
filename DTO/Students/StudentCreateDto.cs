using System.ComponentModel.DataAnnotations;

namespace UniversityApi.DTO.Students;

public class StudentCreateDto
{
    [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress, MaxLength(120)] public string Email { get; set; } = string.Empty;
    [Required] public DateTime? BirthDate { get; set; }
}
