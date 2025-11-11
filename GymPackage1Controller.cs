using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using System;
using System.Threading.Tasks;

namespace Gym.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GymPackage1Controller : ControllerBase
    {
        private readonly IGymPackage1Service _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GymPackage1Controller> _logger;

        public GymPackage1Controller(IGymPackage1Service service, IMapper mapper, ILogger<GymPackage1Controller> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGymPackages(int id = -1)
        {
            try
            {
                var result = await _service.GetGymPackages(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGymPackageById(int id)
        {
            try
            {
                var result = await _service.GetGymPackageById(id);
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
        public async Task<IActionResult> CreateGymPackage(GymPackage1ViewModel model)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.CreateGymPackage(model, username);
                if (result.Success) return CreatedAtAction(nameof(GetGymPackageById), new { id = model.Id }, result);
                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymPackage(int id, GymPackage1ViewModel model)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.UpdateGymPackage(id, model, username);
                if (result.Success) return Ok(result);
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
        public async Task<IActionResult> DeleteGymPackage(int id)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.DeleteGymPackage(id, username);
                if (result.Success) return NoContent();
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
