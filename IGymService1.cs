using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymService1
    {
        Task<ApiResponse<IEnumerable<Gyms1?>>> GetGyms(int id = -1);
        Task<ApiResponse<Gyms1?>> GetGymById(int id);
        Task<ApiResponse<bool>> CreateGym(GymViewModel1 gym, string currentUsername);
        Task<ApiResponse<bool>> UpdateGym(int id, GymViewModel1 gym, string currentUsername);
        Task<ApiResponse<bool>> DeleteGym(int id, string currentUsername);
    }
}
