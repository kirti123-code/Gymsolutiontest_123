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
    public class GymCloseDatesController : ControllerBase
    {
        private readonly IGymCloseDatesService _gymCloseDatesService;
        private readonly IMapper _mapper;
        private readonly ILogger<GymCloseDatesController> _logger;

        public GymCloseDatesController(IGymCloseDatesService gymCloseDatesService, IMapper mapper, ILogger<GymCloseDatesController> logger)
        {
            _gymCloseDatesService = gymCloseDatesService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/GymCloseDates/GetGymCloseDates
        [HttpGet]
        public async Task<IActionResult> GetGymCloseDates(int id = -1)
        {
            try
            
            {
                var result = await _gymCloseDatesService.GetGymCloseDates(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        // GET: api/GymCloseDates/GetGymCloseDateById/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGymCloseDateById(int id)
        {
            try
            {
                var result = await _gymCloseDatesService.GetGymCloseDateById(id);
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

        // POST: api/GymCloseDates/CreateGymCloseDate
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGymCloseDate(GymCloseDatesViewModel model)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _gymCloseDatesService.CreateGymCloseDate(model, currentUsername);

                if (result.Success)
                    return CreatedAtAction(nameof(GetGymCloseDateById), new { id = model.Id }, result);

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
        }

        // PUT: api/GymCloseDates/UpdateGymCloseDate/5
        [HttpPut("{id}")]
     
        public async Task<IActionResult> UpdateGymCloseDate(int id, GymCloseDatesViewModel model)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _gymCloseDatesService.UpdateGymCloseDate(id, model, currentUsername);

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

        // DELETE: api/GymCloseDates/DeleteGymCloseDate/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGymCloseDate(int id)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string ?? "system";

                var result = await _gymCloseDatesService.DeleteGymCloseDate(id, currentUsername);

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
