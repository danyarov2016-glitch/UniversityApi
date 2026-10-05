using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Repositories.Implementations;
public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _db;
    public EnrollmentRepository(ApplicationDbContext db) => _db = db;
    public Task<List<Enrollment>> GetAllAsync() => _db.Enrollments.Include(x => x.Student).Include(x => x.Course).AsNoTracking().ToListAsync();
    public Task<Enrollment?> GetByIdAsync(int id) => _db.Enrollments.Include(x => x.Student).Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);
    public Task<bool> ExistsAsync(int studentId, int courseId) => _db.Enrollments.AnyAsync(x => x.StudentId == studentId && x.CourseId == courseId);
    public async Task AddAsync(Enrollment enrollment) { _db.Enrollments.Add(enrollment); await _db.SaveChangesAsync(); }
    public async Task UpdateAsync(Enrollment enrollment) { _db.Enrollments.Update(enrollment); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(Enrollment enrollment) { _db.Enrollments.Remove(enrollment); await _db.SaveChangesAsync(); }
    public Task<bool> StudentExistsAsync(int studentId) => _db.Students.AnyAsync(x => x.Id == studentId);
    public Task<bool> CourseExistsAsync(int courseId) => _db.Courses.AnyAsync(x => x.Id == courseId);
}
