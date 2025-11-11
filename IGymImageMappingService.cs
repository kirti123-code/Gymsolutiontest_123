
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymImageMappingService
    {
        Task<ApiResponse<IEnumerable<object>>> GetGymImageMappings(int gymId = -1);
        Task<ApiResponse<GymImageMapping?>> GetGymImageMappingById(int id);
        Task<ApiResponse<bool>> CreateGymImageMapping(GymImageMappingViewModel model, string currentUsername);
        Task<ApiResponse<bool>> UpdateGymImageMapping(int id, GymImageMappingViewModel model, string currentUsername);
        Task<ApiResponse<bool>> DeleteGymImageMapping(int id, string currentUsername);
    }


}

