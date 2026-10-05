using System.ComponentModel.DataAnnotations;
namespace UniversityApi.DTO.Courses;
public class CourseCreateDto
{
    [Required, MaxLength(120)] public string Name { get; set; } = string.Empty;
    [MaxLength(500)] public string Description { get; set; } = string.Empty;
    [Range(1, 30)] public int Credits { get; set; }
    [Range(1, int.MaxValue)] public int TeacherId { get; set; }
}
