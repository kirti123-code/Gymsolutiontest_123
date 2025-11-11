// API/Controllers/GymCloseReasonController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class GymCloseReasonController : BaseController
    {
        private readonly IGymCloseReasonService _reasonService;
        private readonly IMapper _mapper;
        private readonly ILogger<GymCloseReasonController> _logger;

        public GymCloseReasonController(
            IGymCloseReasonService reasonService,
            IMapper mapper,
            ILogger<GymCloseReasonController> logger)
        {
            _reasonService = reasonService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllReasons()
      {
            try
            {
                var result = await _reasonService.GetAllReasons();
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetReasonByDate(string date)
        {
            try
            {
                var parsed = DateTime.Parse(date);
                var result = await _reasonService.GetReasonByDate(parsed);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Invalid date or error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateReason(GymCloseReasonViewModel model)
        {
            try
            {
                var user = HttpContext.Items["CurrentUsername"] as string;
                var result = await _reasonService.CreateReason(model, user);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Failed to create reason.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReason(int id, GymCloseReasonViewModel model)
        {
            try
            {
                var user = HttpContext.Items["CurrentUsername"] as string;
                var result = await _reasonService.UpdateReason(id, model, user);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Failed to update reason.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReason(int id)
        {
            try
            {
                var user = HttpContext.Items["CurrentUsername"] as string;
                var result = await _reasonService.DeleteReason(id, user);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Failed to delete reason.");
            }
        }
    }
}
