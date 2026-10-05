using Microsoft.AspNetCore.Mvc;
using UniversityApi.Common;
using UniversityApi.DTO.Enrollments;
using UniversityApi.Services;
namespace UniversityApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollmentService _service; private readonly ILogger<EnrollmentsController> _logger;
    public EnrollmentsController(EnrollmentService service,ILogger<EnrollmentsController> logger){_service=service;_logger=logger;}
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(ReturnResult<List<EnrollmentDto>>.Success(await _service.GetAllAsync(),200,HttpContext.TraceIdentifier));
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id){var d=await _service.GetByIdAsync(id);if(d is null){_logger.LogWarning("Enrollment not found: {EnrollmentId}",id);return NotFound(ReturnResult<object>.Failure(404,"ENROLLMENT_NOT_FOUND","Enrollment not found",HttpContext.TraceIdentifier));}return Ok(ReturnResult<EnrollmentDto>.Success(d,200,HttpContext.TraceIdentifier));}
    [HttpPost] public async Task<IActionResult> Create(EnrollmentCreateDto dto){var r=await _service.CreateAsync(dto);if(!r.Success){var code=r.ErrorCode!;return code=="ENROLLMENT_CONFLICT"?Conflict(ReturnResult<object>.Failure(409,code,r.ErrorMessage!,HttpContext.TraceIdentifier)):NotFound(ReturnResult<object>.Failure(404,code,r.ErrorMessage!,HttpContext.TraceIdentifier));}return CreatedAtAction(nameof(GetById),new{id=r.Data!.Id},ReturnResult<EnrollmentDto>.Success(r.Data,201,HttpContext.TraceIdentifier));}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,EnrollmentUpdateDto dto){var r=await _service.UpdateAsync(id,dto);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<EnrollmentDto>.Success(r.Data!,200,HttpContext.TraceIdentifier));}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var r=await _service.DeleteAsync(id);if(!r.Success)return NotFound(ReturnResult<object>.Failure(404,r.ErrorCode!,r.ErrorMessage!,HttpContext.TraceIdentifier));return Ok(ReturnResult<object>.Success(new{message="Enrollment deleted"},200,HttpContext.TraceIdentifier));}
}
