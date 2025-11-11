using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using System;
using System.Threading.Tasks;

namespace Student.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentMasterController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentMasterController> _logger;

        public StudentMasterController(IStudentService studentService, IMapper mapper, ILogger<StudentMasterController> logger)
        {
            _studentService = studentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(int id = -1)
        {
            try
            {
                var result = await _studentService.GetStudents(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var result = await _studentService.GetStudentById(id);
                if (result.Data == null)
                    return NotFound(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateStudent(StudentViewModel student)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _studentService.CreateStudent(student, currentUsername);

                if (result.Success)
                    return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, result);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStudent(int id, StudentViewModel student)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _studentService.UpdateStudent(id, student, currentUsername);

                if (result.Success)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _studentService.DeleteStudent(id, currentUsername);

                if (result.Success)
                    return NoContent();

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }
    }
}
