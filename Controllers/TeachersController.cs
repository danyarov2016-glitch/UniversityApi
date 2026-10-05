using Microsoft.AspNetCore.Mvc;
using UniversityApi.Common;
using UniversityApi.DTO.Teachers;
using UniversityApi.Services;
namespace UniversityApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly TeacherService _service; private readonly ILogger<TeachersController> _logger;
    public TeachersController(TeacherService service,ILogger<TeachersController> logger){_service=service;_logger=logger;}
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(ReturnResult<List<TeacherDto>>.Success(await _service.GetAllAsync(),200,HttpContext.TraceIdentifier));
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id){var d=await _service.GetByIdAsync(id);if(d is null){_logger.LogWarning("Teacher not found: {TeacherId}",id);return NotFound(ReturnResult<object>.Failure(404,"TEACHER_NOT_FOUND","Teacher not found",HttpContext.TraceIdentifier));}return Ok(ReturnResult<TeacherDto>.Success(d,200,HttpContext.TraceIdentifier));}
    [HttpPost] public async Task<IActionResult> Create(TeacherCreateDto dto){var r=await _service.CreateAsync(dto);if(!r.Success)return Conflict(ReturnResult<object>.Failure(409,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return CreatedAtAction(nameof(GetById),new{id=r.Data!.Id},ReturnResult<TeacherDto>.Success(r.Data,201,HttpContext.TraceIdentifier));}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,TeacherUpdateDto dto){var r=await _service.UpdateAsync(id,dto);if(!r.Success){if(r.ErrorCode=="TEACHER_NOT_FOUND")return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Conflict(ReturnResult<object>.Failure(409,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));}return Ok(ReturnResult<TeacherDto>.Success(r.Data!,200,HttpContext.TraceIdentifier));}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var r=await _service.DeleteAsync(id);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<object>.Success(new{message="Teacher deleted"},200,HttpContext.TraceIdentifier));}
}
