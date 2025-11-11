using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class PatientMasterController : BaseController
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientMasterController> _logger;

        public PatientMasterController(IPatientService patientService, IMapper mapper, ILogger<PatientMasterController> logger)
        {
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPatientDetails(int id = -1)
        {
            try
            {
                var result = await _patientService.GetPatientDetails(id);
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
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var result = await _patientService.GetPatientById(id);
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
        public async Task<IActionResult> CreatePatient(PatientViewModel patient)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _patientService.CreatePatient(patient, currentUser);
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
        public async Task<IActionResult> UpdatePatient(int id, PatientViewModel patient)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _patientService.UpdatePatient(id, patient, currentUser);
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
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _patientService.DeletePatient(id, currentUser);
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
