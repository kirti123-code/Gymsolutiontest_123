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
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;
        private readonly IMapper _mapper;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationService registrationService, IMapper mapper, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRegistrations(int id = -1)
        {
            try
            {
                var result = await _registrationService.GetRegistrations(id);
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
        public async Task<IActionResult> GetRegistrationById(int id)
        {
            try
            {
                var result = await _registrationService.GetRegistrationById(id);
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
        public async Task<IActionResult> CreateRegistration(RegistrationViewModel registration)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _registrationService.CreateRegistration(registration, currentUser);
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
        public async Task<IActionResult> UpdateRegistration(int id, RegistrationViewModel registration)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _registrationService.UpdateRegistration(id, registration, currentUser);
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
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _registrationService.DeleteRegistration(id, currentUser);
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
