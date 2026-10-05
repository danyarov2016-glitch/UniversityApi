using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Repositories.Implementations;
public class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationDbContext _db;
    public TeacherRepository(ApplicationDbContext db) => _db = db;
    public Task<List<Teacher>> GetAllAsync() => _db.Teachers.AsNoTracking().ToListAsync();
    public Task<Teacher?> GetByIdAsync(int id) => _db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
    public Task<Teacher?> GetByEmailAsync(string email) => _db.Teachers.FirstOrDefaultAsync(x => x.Email == email);
    public async Task AddAsync(Teacher teacher) { _db.Teachers.Add(teacher); await _db.SaveChangesAsync(); }
    public async Task UpdateAsync(Teacher teacher) { _db.Teachers.Update(teacher); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(Teacher teacher) { _db.Teachers.Remove(teacher); await _db.SaveChangesAsync(); }
}
