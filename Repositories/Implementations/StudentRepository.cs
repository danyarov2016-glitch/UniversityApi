using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Repositories.Implementations;
public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _db;
    public StudentRepository(ApplicationDbContext db) => _db = db;
    public Task<List<Student>> GetAllAsync() => _db.Students.AsNoTracking().ToListAsync();
    public Task<Student?> GetByIdAsync(int id) => _db.Students.FirstOrDefaultAsync(x => x.Id == id);
    public Task<Student?> GetWithCoursesAsync(int id) => _db.Students.Include(x => x.Enrollments).ThenInclude(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);
    public Task<Student?> GetByEmailAsync(string email) => _db.Students.FirstOrDefaultAsync(x => x.Email == email);
    public async Task AddAsync(Student student) { _db.Students.Add(student); await _db.SaveChangesAsync(); }
    public async Task UpdateAsync(Student student) { _db.Students.Update(student); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(Student student) { _db.Students.Remove(student); await _db.SaveChangesAsync(); }
}
