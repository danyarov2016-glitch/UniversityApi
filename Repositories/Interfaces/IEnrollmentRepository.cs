using UniversityApi.Models;
namespace UniversityApi.Repositories.Interfaces;
public interface IEnrollmentRepository
{
    Task<List<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int studentId, int courseId);
    Task AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task DeleteAsync(Enrollment enrollment);
    Task<bool> StudentExistsAsync(int studentId);
    Task<bool> CourseExistsAsync(int courseId);
}
