using AutoMapper;
using UniversityApi.DTO.Courses;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Services;
public class CourseService
{
    private readonly ICourseRepository _repo; private readonly IMapper _mapper; private readonly ILogger<CourseService> _logger;
    public CourseService(ICourseRepository repo,IMapper mapper,ILogger<CourseService> logger){_repo=repo;_mapper=mapper;_logger=logger;}
    public async Task<List<CourseDto>> GetAllAsync(string? search)=>_mapper.Map<List<CourseDto>>(await _repo.GetAllAsync(search));
    public async Task<CourseDto?> GetByIdAsync(int id)=>_mapper.Map<CourseDto?>(await _repo.GetByIdAsync(id));
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage,CourseDto? Data)> CreateAsync(CourseCreateDto dto)
    {if(!await _repo.TeacherExistsAsync(dto.TeacherId))return(false,"TEACHER_NOT_FOUND","Teacher not found",null);var e=_mapper.Map<Course>(dto);e.CreatedAt=DateTime.UtcNow;await _repo.AddAsync(e);_logger.LogInformation("Course created: {CourseId}",e.Id);return(true,null,null,_mapper.Map<CourseDto>(await _repo.GetByIdAsync(e.Id)));}
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage,CourseDto? Data)> UpdateAsync(int id,CourseUpdateDto dto)
    {var e=await _repo.GetByIdAsync(id);if(e is null)return(false,"COURSE_NOT_FOUND","Course not found",null);if(!await _repo.TeacherExistsAsync(dto.TeacherId))return(false,"TEACHER_NOT_FOUND","Teacher not found",null);_mapper.Map(dto,e);await _repo.UpdateAsync(e);_logger.LogInformation("Course updated: {CourseId}",id);return(true,null,null,_mapper.Map<CourseDto>(await _repo.GetByIdAsync(id)));}
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage)> DeleteAsync(int id)
    {var e=await _repo.GetByIdAsync(id);if(e is null)return(false,"COURSE_NOT_FOUND","Course not found");await _repo.DeleteAsync(e);_logger.LogInformation("Course deleted: {CourseId}",id);return(true,null,null);}
}
