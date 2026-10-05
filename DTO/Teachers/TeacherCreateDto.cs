using System.ComponentModel.DataAnnotations;
namespace UniversityApi.DTO.Teachers;
public class TeacherCreateDto
{
    [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress, MaxLength(120)] public string Email { get; set; } = string.Empty;
    [Required, MaxLength(100)] public string Department { get; set; } = string.Empty;
}
