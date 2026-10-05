using AutoMapper;
using UniversityApi.DTO.Enrollments;
using UniversityApi.Models;
using UniversityApi.Repositories.Interfaces;
namespace UniversityApi.Services;
public class EnrollmentService
{
    private readonly IEnrollmentRepository _repo; private readonly IMapper _mapper; private readonly ILogger<EnrollmentService> _logger;
    public EnrollmentService(IEnrollmentRepository repo,IMapper mapper,ILogger<EnrollmentService> logger){_repo=repo;_mapper=mapper;_logger=logger;}
    public async Task<List<EnrollmentDto>> GetAllAsync()=>_mapper.Map<List<EnrollmentDto>>(await _repo.GetAllAsync());
    public async Task<EnrollmentDto?> GetByIdAsync(int id)=>_mapper.Map<EnrollmentDto?>(await _repo.GetByIdAsync(id));
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage,EnrollmentDto? Data)> CreateAsync(EnrollmentCreateDto dto)
    {if(!await _repo.StudentExistsAsync(dto.StudentId))return(false,"STUDENT_NOT_FOUND","Student not found",null);if(!await _repo.CourseExistsAsync(dto.CourseId))return(false,"COURSE_NOT_FOUND","Course not found",null);if(await _repo.ExistsAsync(dto.StudentId,dto.CourseId))return(false,"ENROLLMENT_CONFLICT","Student is already enrolled in this course",null);var e=_mapper.Map<Enrollment>(dto);e.EnrollmentDate=DateTime.UtcNow;await _repo.AddAsync(e);_logger.LogInformation("Enrollment created: {EnrollmentId}",e.Id);return(true,null,null,_mapper.Map<EnrollmentDto>(await _repo.GetByIdAsync(e.Id)));}
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage,EnrollmentDto? Data)> UpdateAsync(int id,EnrollmentUpdateDto dto)
    {var e=await _repo.GetByIdAsync(id);if(e is null)return(false,"ENROLLMENT_NOT_FOUND","Enrollment not found",null);e.Grade=dto.Grade;await _repo.UpdateAsync(e);_logger.LogInformation("Enrollment updated: {EnrollmentId}",id);return(true,null,null,_mapper.Map<EnrollmentDto>(await _repo.GetByIdAsync(id)));}
    public async Task<(bool Success,string? ErrorCode,string? ErrorMessage)> DeleteAsync(int id)
    {var e=await _repo.GetByIdAsync(id);if(e is null)return(false,"ENROLLMENT_NOT_FOUND","Enrollment not found");await _repo.DeleteAsync(e);_logger.LogInformation("Enrollment deleted: {EnrollmentId}",id);return(true,null,null);}
}
