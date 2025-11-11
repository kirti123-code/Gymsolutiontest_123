using Microsoft.AspNetCore.Mvc;
using MODELS.ViewModels;

namespace API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Response<T>(T data, string? message = null,int totalRecords = 0)
        {
            var response = new ApiResponse<T>(data, message, totalRecords);
            return Ok(response);
        }

        protected IActionResult ErrorResponse(string error)
        {
            var response = new ApiResponse<object>(error);
            return Ok(response);
        }
    }
}
