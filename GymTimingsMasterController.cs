using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GymTimingsMasterController : BaseController
    {
        private readonly IGymTimingsService1 _gymTimingsService;
        private readonly IMapper _mapper;
        private readonly ILogger<GymTimingsMasterController> _logger;

        public GymTimingsMasterController(
            IGymTimingsService1 gymTimingsService,
            IMapper mapper,
            ILogger<GymTimingsMasterController> logger)
        {
            _gymTimingsService = gymTimingsService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGymTimings(int id = -1)
        {
            try
            {
                var res = await _gymTimingsService.GetGymTimings(id);
                return Response(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching gym timings");
                return ErrorResponse("Something went wrong while fetching gym timings.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGymTimingById(int id)
        {
            try
            {
                var res = await _gymTimingsService.GetGymTimingById(id);
                return Response(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching gym timing by ID");
                return ErrorResponse("Something went wrong while fetching gym timing by ID.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGymTiming([FromBody] GymTimingsViewModel1 model)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"]?.ToString() ?? "System";
                var res = await _gymTimingsService.CreateGymTiming(model, currentUser);
                return Response(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating gym timing");
                return ErrorResponse("Something went wrong while creating gym timing.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymTiming(int id, [FromBody] GymTimingsViewModel1 model)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"]?.ToString() ?? "System";
                var res = await _gymTimingsService.UpdateGymTiming(id, model, currentUser);
                return Response(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating gym timing");
                return ErrorResponse("Something went wrong while updating gym timing.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymTiming(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"]?.ToString() ?? "System";
                var res = await _gymTimingsService.DeleteGymTiming(id, currentUser);
                return Response(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting gym timing");
                return ErrorResponse("Something went wrong while deleting gym timing.");
            }
        }
    }
}
