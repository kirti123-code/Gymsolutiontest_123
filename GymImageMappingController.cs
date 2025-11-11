
using API.Filters.ActionFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES;
using SERVICES.Interfaces;
using System;

namespace API.Controllers

{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GymImageMappingController : ControllerBase
    {
        private readonly IGymImageMappingService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GymImageMappingController> _logger;

        public GymImageMappingController(IGymImageMappingService service, IMapper mapper, ILogger<GymImageMappingController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGymImageMappings(int gymId = -1)
        {
            try
            {
                var result = await _service.GetGymImageMappings(gymId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGymImageMappingById(int id)
        {
            try
            {
                var result = await _service.GetGymImageMappingById(id);
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
        public async Task<IActionResult> CreateGymImageMapping(GymImageMappingViewModel model)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.CreateGymImageMapping(model, username);
                if (result.Success) return CreatedAtAction(nameof(GetGymImageMappingById), new { id = model }, result);
                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymImageMapping(int id, GymImageMappingViewModel model)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.UpdateGymImageMapping(id, model, username);
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
        public async Task<IActionResult> DeleteGymImageMapping(int id)
        {
            try
            {
                var username = HttpContext.Items["CurrentUsername"] as string ?? "system";
                var result = await _service.DeleteGymImageMapping(id, username);
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
