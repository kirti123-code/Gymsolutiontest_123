using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.ViewModels;
using SERVICES.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LabReportController : ControllerBase
    {
        private readonly ILabReportService _labReportService;
        private readonly IMapper _mapper;

        public LabReportController(ILabReportService labReportService, IMapper mapper)
        {
            _labReportService = labReportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports(int id = -1)
        {
            var result = await _labReportService.GetReports(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var result = await _labReportService.GetReportById(id);
            if (result.Data == null)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateReport(LabReportDetailViewModel model)
        {
            var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";
            var result = await _labReportService.CreateReport(model, currentUsername);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReport(int id, LabReportDetailViewModel model)
        {
            var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";
            var result = await _labReportService.UpdateReport(id, model, currentUsername);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";
            var result = await _labReportService.DeleteReport(id, currentUsername);
            if (result.Success)
                return NoContent();
            return BadRequest(result.Message);
        }
    }
}
