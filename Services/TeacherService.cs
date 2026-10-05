using AutoMapper;
using UniversityApi.DTO.Teachers;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Services;
public class TeacherService
{
    private readonly ITeacherRepository _repo; private readonly IMapper _mapper; private readonly ILogger<TeacherService> _logger;
    public TeacherService(ITeacherRepository repo, IMapper mapper, ILogger<TeacherService> logger) { _repo = repo; _mapper = mapper; _logger = logger; }
    public async Task<List<TeacherDto>> GetAllAsync() => _mapper.Map<List<TeacherDto>>(await _repo.GetAllAsync());
    public async Task<TeacherDto?> GetByIdAsync(int id) => _mapper.Map<TeacherDto?>(await _repo.GetByIdAsync(id));
    public async Task<(bool Success, string? ErrorCode, string? ErrorMessage, TeacherDto? Data)> CreateAsync(TeacherCreateDto dto)
    { if (await _repo.GetByEmailAsync(dto.Email) is not null) return (false,"EMAIL_CONFLICT","A teacher with this email already exists",null); var e=_mapper.Map<Teacher>(dto); await _repo.AddAsync(e); _logger.LogInformation("Teacher created: {TeacherId}",e.Id); return (true,null,null,_mapper.Map<TeacherDto>(e)); }
    public async Task<(bool Success, string? ErrorCode, string? ErrorMessage, TeacherDto? Data)> UpdateAsync(int id, TeacherUpdateDto dto)
    { var e=await _repo.GetByIdAsync(id); if(e is null)return(false,"TEACHER_NOT_FOUND","Teacher not found",null); var other=await _repo.GetByEmailAsync(dto.Email); if(other is not null&&other.Id!=id)return(false,"EMAIL_CONFLICT","A teacher with this email already exists",null); _mapper.Map(dto,e); await _repo.UpdateAsync(e); _logger.LogInformation("Teacher updated: {TeacherId}",id); return(true,null,null,_mapper.Map<TeacherDto>(e)); }
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage)> DeleteAsync(int id)
    { var e=await _repo.GetByIdAsync(id); if(e is null)return(false,"TEACHER_NOT_FOUND","Teacher not found"); await _repo.DeleteAsync(e); _logger.LogInformation("Teacher deleted: {TeacherId}",id); return(true,null,null); }
}
