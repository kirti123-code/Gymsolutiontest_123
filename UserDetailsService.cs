using AutoMapper;
using DAL;
using MODELS;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using SERVICES.Repository;
using Microsoft.EntityFrameworkCore;

namespace SERVICES
{
    public class UserDetailsService : Repository<UserDetail>, IUserDetailsService
    {
        private readonly IMapper _mapper;

        public UserDetailsService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UserDetail?>>> GetUserDetails(int id = -1)
        {
            try
            {
                var response = new ApiResponse<IEnumerable<UserDetail?>>(false);
                if (id == -1)
                {
                    var data = await GetAllAsync();

                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all user details successfully.";
                    return response;
                }
                else
                {
                    var user = await GetByIdAsync(id);
                    var data = new List<UserDetail?> { user };

                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched user details successfully.";
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<UserDetail?>> GetMyProfile(int id)
        {
            try
            {
                var response = new ApiResponse<UserDetail?>(false);
                var data = await GetByIdAsync(id);

                if (data != null)
                {
                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched user profile successfully.";
                }
                else
                {
                    response.Message = "User not found.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeleteUser(int userId, string currentUsername)
        {
            try
            {
                ApiResponse<bool> response = new ApiResponse<bool>(false);

                var user = await this.GetByIdAsync(userId);
                if (user != null)
                {
                    user.UpdatedBy = currentUsername;
                    user.UpdatedOn = DateTime.UtcNow;
                    var data = await this.DeleteAsync(userId);
                    response.Data = data;
                    response.Success = true;
                    response.Message = data ? "User deleted successfully." : "Failed to delete user.";

                }
                if (user == null)
                {
                    response.Message = "User not found.";
                    response.Success = false;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateUser(int userId, MyProfileViewModel userDetail, string currentUsername)
        {
            try
            {
                ApiResponse<bool> response = new ApiResponse<bool>(false);

                var user = await this.GetByIdAsync(userId);
                if (user != null)
                {
                    _mapper.Map(userDetail, user);

                    user.UpdatedBy = currentUsername;
                    user.UpdatedBy = currentUsername;
                    user.UpdatedOn = DateTime.UtcNow;
                    var data = await this.UpdateAsync(user);
                    response.Data = data;
                    response.Success = true;
                    response.Message = data ? "User updated successfully." : "Failed to update user.";

                }
                if (user == null)
                {
                    response.Message = "User not found.";
                    response.Success = false;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
