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
    public class DesignationMasterController : BaseController
    {
        private readonly IDesignationService _designationService;
        private readonly IMapper _mapper;
        private readonly ILogger<DesignationMasterController> _logger;

        public DesignationMasterController(IDesignationService designationService, IMapper mapper, ILogger<DesignationMasterController> logger)
        {
            _designationService = designationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetDesignations(int id = -1)
        {
            try
            {
                var result = await _designationService.GetDesignations(id);
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
        public async Task<IActionResult> GetDesignationById(int id)
        {
            try
            {
                var result = await _designationService.GetDesignationById(id);
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
        public async Task<IActionResult> CreateDesignation(DesignationViewModel designation)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _designationService.CreateDesignation(designation, currentUser);
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
        public async Task<IActionResult> UpdateDesignation(int id, DesignationViewModel designation)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _designationService.UpdateDesignation(id, designation, currentUser);
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
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            try
            {
                var currentUser = HttpContext.Items["CurrentUsername"] as string;
                var result = await _designationService.DeleteDesignation(id, currentUser);
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
