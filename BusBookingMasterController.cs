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
    public class BusBookingMasterController : BaseController
    {
        private readonly IBusBookingService _busBookingService;
        private readonly IMapper _mapper;
        private readonly ILogger<BusBookingMasterController> _logger;

        public BusBookingMasterController(IBusBookingService busBookingService, IMapper mapper, ILogger<BusBookingMasterController> logger)
        {
            _busBookingService = busBookingService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetBusBookings(int id = -1)
        {
            try
            {
                var result = await _busBookingService.GetBusBookingDetails(id);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetBusBookingById(int id)
        {
            try
            {
                var result = await _busBookingService.GetBusBookingById(id);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBusBooking(BusBookingViewModel booking)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _busBookingService.CreateBusBooking(booking, currentUser);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBusBooking(int id, BusBookingViewModel booking)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _busBookingService.UpdateBusBooking(id, booking, currentUser);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBusBooking(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _busBookingService.DeleteBusBooking(id, currentUser);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }
    }
}
