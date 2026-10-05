using Microsoft.AspNetCore.Mvc;
using UniversityApi.Common;
using UniversityApi.DTO.Students;
using UniversityApi.Services;
namespace UniversityApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _service; private readonly ILogger<StudentsController> _logger;
    public StudentsController(StudentService service,ILogger<StudentsController> logger){_service=service;_logger=logger;}
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(ReturnResult<List<StudentDto>>.Success(await _service.GetAllAsync(),200,HttpContext.TraceIdentifier));
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id){var data=await _service.GetByIdAsync(id);if(data is null){_logger.LogWarning("Student not found: {StudentId}",id);return NotFound(ReturnResult<object>.Failure(404,"STUDENT_NOT_FOUND","Student not found",HttpContext.TraceIdentifier));}return Ok(ReturnResult<StudentDto>.Success(data,200,HttpContext.TraceIdentifier));}
    [HttpGet("{id:int}/courses")] public async Task<IActionResult> GetWithCourses(int id){var data=await _service.GetByIdAsync(id);if(data is null)return NotFound(ReturnResult<object>.Failure(404,"STUDENT_NOT_FOUND","Student not found",HttpContext.TraceIdentifier));var entity=await _service.GetWithCoursesAsync(id);return Ok(ReturnResult<object>.Success(new{student=data,courses=entity?.Enrollments.Select(e=>new{e.CourseId,e.Course!.Name,e.Course.Credits,e.Grade})},200,HttpContext.TraceIdentifier));}
    [HttpPost] public async Task<IActionResult> Create(StudentCreateDto dto){var r=await _service.CreateAsync(dto);if(!r.Success)return Conflict(ReturnResult<object>.Failure(409,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return CreatedAtAction(nameof(GetById),new{id=r.Data!.Id},ReturnResult<StudentDto>.Success(r.Data,201,HttpContext.TraceIdentifier));}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,StudentUpdateDto dto){var r=await _service.UpdateAsync(id,dto);if(!r.Success){if(r.ErrorCode=="STUDENT_NOT_FOUND")return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Conflict(ReturnResult<object>.Failure(409,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));}return Ok(ReturnResult<StudentDto>.Success(r.Data!,200,HttpContext.TraceIdentifier));}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var r=await _service.DeleteAsync(id);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<object>.Success(new{message="Student deleted"},200,HttpContext.TraceIdentifier));}
}
