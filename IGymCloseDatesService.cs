
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymCloseDatesService
    {
        // ✅ Changed type to IEnumerable<object> to match updated service
        Task<ApiResponse<IEnumerable<object>>> GetGymCloseDates(int id = -1);

        Task<ApiResponse<GymCloseDates?>> GetGymCloseDateById(int id);
        Task<ApiResponse<bool>> CreateGymCloseDate(GymCloseDatesViewModel model, string currentUsername);
        Task<ApiResponse<bool>> UpdateGymCloseDate(int id, GymCloseDatesViewModel model, string currentUsername);
        Task<ApiResponse<bool>> DeleteGymCloseDate(int id, string currentUsername);
    }
}
