using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Repositories.Implementations;
public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _db;
    public CourseRepository(ApplicationDbContext db) => _db = db;
    public Task<List<Course>> GetAllAsync(string? search)
    {
        var query = _db.Courses.Include(x => x.Teacher).AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        return query.ToListAsync();
    }
    public Task<Course?> GetByIdAsync(int id) => _db.Courses.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id == id);
    public async Task AddAsync(Course course) { _db.Courses.Add(course); await _db.SaveChangesAsync(); }
    public async Task UpdateAsync(Course course) { _db.Courses.Update(course); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(Course course) { _db.Courses.Remove(course); await _db.SaveChangesAsync(); }
    public Task<bool> TeacherExistsAsync(int teacherId) => _db.Teachers.AnyAsync(x => x.Id == teacherId);
}
