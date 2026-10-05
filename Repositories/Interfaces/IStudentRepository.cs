using UniversityApi.Models;
namespace UniversityApi.Repositories.Interfaces;
public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetWithCoursesAsync(int id);
    Task<Student?> GetByEmailAsync(string email);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
}
