using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using System;
using System.Threading.Tasks;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeMasterController> _logger;

        public EmployeeMasterController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeeMasterController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }





        // GET: api/EmployeeMaster/GetEmployees
        [HttpGet]
        public async Task<IActionResult> GetEmployees(int id = -1)
        {
            try
            {
                var result = await _employeeService.GetEmployees(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500,"Something went wrong. Please try again later.");
            }
        }

        // GET: api/EmployeeMaster/GetEmployeeById/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);
                if (result.Data == null)
                    return NotFound(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later");
            }
        }

        // POST: api/EmployeeMaster/CreateEmployee
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel employee)
        {
            try
            {
                // Assuming CurrentUsername comes from your authentication context:
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _employeeService.CreateEmployee(employee, currentUsername);

                if (result.Success)
                    return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, result);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        // PUT: api/EmployeeMaster/UpdateEmployee/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel employee)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _employeeService.UpdateEmployee(id, employee, currentUsername);

                if (result.Success)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode( 500, "Something went wrong. Please try again later.");
            }
        }

        // DELETE: api/EmployeeMaster/DeleteEmployee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _employeeService.DeleteEmployee(id, currentUsername);

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
