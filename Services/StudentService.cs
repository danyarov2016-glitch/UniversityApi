using AutoMapper;
using UniversityApi.DTO.Students;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Services;
public class StudentService
{
    private readonly IStudentRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;
    public StudentService(IStudentRepository repo, IMapper mapper, ILogger<StudentService> logger) { _repo = repo; _mapper = mapper; _logger = logger; }
    public async Task<List<StudentDto>> GetAllAsync() => _mapper.Map<List<StudentDto>>(await _repo.GetAllAsync());
    public async Task<StudentDto?> GetByIdAsync(int id) => _mapper.Map<StudentDto?>(await _repo.GetByIdAsync(id));
    public Task<Student?> GetWithCoursesAsync(int id) => _repo.GetWithCoursesAsync(id);
    public async Task<(bool Success, string? ErrorCode, string? ErrorMessage, StudentDto? Data)> CreateAsync(StudentCreateDto dto)
    {
        if (await _repo.GetByEmailAsync(dto.Email) is not null) return (false, "EMAIL_CONFLICT", "A student with this email already exists", null);
        var entity = _mapper.Map<Student>(dto); entity.CreatedAt = DateTime.UtcNow; await _repo.AddAsync(entity); _logger.LogInformation("Student created: {StudentId}", entity.Id); return (true, null, null, _mapper.Map<StudentDto>(entity));
    }
    public async Task<(bool Success, string? ErrorCode, string? ErrorMessage, StudentDto? Data)> UpdateAsync(int id, StudentUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id); if (entity is null) return (false, "STUDENT_NOT_FOUND", "Student not found", null);
        var other = await _repo.GetByEmailAsync(dto.Email); if (other is not null && other.Id != id) return (false, "EMAIL_CONFLICT", "A student with this email already exists", null);
        _mapper.Map(dto, entity); await _repo.UpdateAsync(entity); _logger.LogInformation("Student updated: {StudentId}", id); return (true, null, null, _mapper.Map<StudentDto>(entity));
    }
    public async Task<(bool Success, string? ErrorCode, string? ErrorMessage)> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id); if (entity is null) return (false, "STUDENT_NOT_FOUND", "Student not found");
        await _repo.DeleteAsync(entity); _logger.LogInformation("Student deleted: {StudentId}", id); return (true, null, null);
    }
}
