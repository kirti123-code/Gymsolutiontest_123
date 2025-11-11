using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymPackage1Service
    {
        Task<ApiResponse<IEnumerable<object>>> GetGymPackages(int id = -1);
        Task<ApiResponse<GymPackage1?>> GetGymPackageById(int id);
        Task<ApiResponse<bool>> CreateGymPackage(GymPackage1ViewModel model, string currentUsername);
        Task<ApiResponse<bool>> UpdateGymPackage(int id, GymPackage1ViewModel model, string currentUsername);
        Task<ApiResponse<bool>> DeleteGymPackage(int id, string currentUsername);
    }
}
