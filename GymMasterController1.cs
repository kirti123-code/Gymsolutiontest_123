using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GymMasterController : BaseController
    {
        private readonly IGymService1 _gymService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ILogger<GymMasterController> _logger;

        public GymMasterController(AppDbContext context ,IGymService1 gymService, IMapper mapper, ILogger<GymMasterController> logger)
        {
            _gymService = gymService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetGyms(int id = -1)
        {
            try
            {
                var result = await _gymService.GetGyms(id);
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
        public async Task<IActionResult> GetGymById(int id)
        {
            try
            {
                var result = await _gymService.GetGymById(id);
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
        public async Task<IActionResult> CreateGym(GymViewModel1 gym)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _gymService.CreateGym(gym, currentUser);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGym(int id, GymViewModel1 gym)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _gymService.UpdateGym(id, gym, currentUser);
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
        public async Task<IActionResult> DeleteGym(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _gymService.DeleteGym(id, currentUser);
                return Response(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong.");
            }
        }


        [HttpGet]

        public IActionResult GetGymOptions()
        {
            var options = _context.Gyms1
                .Select(g => new { GymId = g.Id, GymName = g.Name })
                .ToList();

            return Ok(new { success = true, data = options });
        }
    }
}
