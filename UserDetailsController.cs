using AutoMapper;
using MODELS.ViewModels; // Add this namespace
using SERVICES.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserDetailsController : BaseController
    {
        private readonly ILogger<UserDetailsController> _logger;
        private readonly IUserDetailsService _userDetailsService;
        private readonly IMapper _mapper;
        public UserDetailsController(IUserDetailsService userDetailsService, ILogger<UserDetailsController> logger, IMapper mapper)
        {
            _logger = logger;
            _userDetailsService = userDetailsService;
            _mapper = mapper;
        }

        //post: api/UserDetails
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(int id, MyProfileViewModel userDetail)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string;

                var results = await _userDetailsService.UpdateUser(id, userDetail, currentUsername);
                return Response(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong. Please try again later.");
            }
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string;
                var userId = Convert.ToInt32(HttpContext.Items["CurrentUserId"]);

                var results = await _userDetailsService.GetMyProfile(userId);
                MyProfileViewModel myProfile = _mapper.Map<MyProfileViewModel>(results.Data);
                return Response(myProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong. Please try again later.");
            }
        }

        // GET: api/UserDetails
        [Authorize(Roles="admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserDetails(int id =-1)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string;

                var results = await _userDetailsService.GetUserDetails(id);

                return Response(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong. Please try again later.");
            }
        }        

        //put: api/UserDetails
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var currentUsername = HttpContext.Items["CurrentUsername"] as string;

                var results = await _userDetailsService.DeleteUser(id, currentUsername);
                return Response(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong. Please try again later.");
            }
        }
    }
}
