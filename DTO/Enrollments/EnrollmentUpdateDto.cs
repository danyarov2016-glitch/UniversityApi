using System.ComponentModel.DataAnnotations;
namespace UniversityApi.DTO.Enrollments;
public class EnrollmentUpdateDto
{
    [Range(0, 100)] public decimal? Grade { get; set; }
}
