using MODELS;
using MODELS.ViewModels;

namespace SERVICES.Interfaces
{
    public interface IUserDetailsService
    {
        Task<ApiResponse<IEnumerable<UserDetail?>>> GetUserDetails(int id=-1);
        Task<ApiResponse<UserDetail?>> GetMyProfile(int id);
        Task<ApiResponse<bool>> DeleteUser(int userId, string currentUsername);
        Task<ApiResponse<bool>> UpdateUser(int userId, MyProfileViewModel userDetail, string currentUsername);
    }
}