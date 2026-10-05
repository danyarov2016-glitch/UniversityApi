using Microsoft.AspNetCore.Mvc;
using UniversityApi.Common;
using UniversityApi.DTO.Courses;
using UniversityApi.Services;
namespace UniversityApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CourseService _service; private readonly ILogger<CoursesController> _logger;
    public CoursesController(CourseService service,ILogger<CoursesController> logger){_service=service;_logger=logger;}
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery]string? search)=>Ok(ReturnResult<List<CourseDto>>.Success(await _service.GetAllAsync(search),200,HttpContext.TraceIdentifier));
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id){var d=await _service.GetByIdAsync(id);if(d is null){_logger.LogWarning("Course not found: {CourseId}",id);return NotFound(ReturnResult<object>.Failure(404,"COURSE_NOT_FOUND","Course not found",HttpContext.TraceIdentifier));}return Ok(ReturnResult<CourseDto>.Success(d,200,HttpContext.TraceIdentifier));}
    [HttpPost] public async Task<IActionResult> Create(CourseCreateDto dto){var r=await _service.CreateAsync(dto);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return CreatedAtAction(nameof(GetById),new{id=r.Data!.Id},ReturnResult<CourseDto>.Success(r.Data,201,HttpContext.TraceIdentifier));}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,CourseUpdateDto dto){var r=await _service.UpdateAsync(id,dto);if(!r.Success)return r.ErrorCode=="COURSE_NOT_FOUND"?NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier)):NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<CourseDto>.Success(r.Data!,200,HttpContext.TraceIdentifier));}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var r=await _service.DeleteAsync(id);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<object>.Success(new{message="Course deleted"},200,HttpContext.TraceIdentifier));}
}
