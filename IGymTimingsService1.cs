using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymTimingsService1
    {
        Task<ApiResponse<IEnumerable<GymTimingsDto>>> GetGymTimings(int id = -1);
        Task<ApiResponse<GymTimings1?>> GetGymTimingById(int id);
        Task<ApiResponse<bool>> CreateGymTiming(GymTimingsViewModel1 vm, string currentUsername);
        Task<ApiResponse<bool>> UpdateGymTiming(int id, GymTimingsViewModel1 vm, string currentUsername);
        Task<ApiResponse<bool>> DeleteGymTiming(int id, string currentUsername);
    }
}
