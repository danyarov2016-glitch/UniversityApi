using UniversityApi.Models;
namespace UniversityApi.Repositories.Interfaces;
public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync(string? search);
    Task<Course?> GetByIdAsync(int id);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
    Task<bool> TeacherExistsAsync(int teacherId);
}
