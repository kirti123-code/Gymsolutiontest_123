using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using SERVICES.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES
{
    public class GymService1 : Repository<Gyms1>, IGymService1
    {
        private readonly IMapper _mapper;

        public GymService1(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<Gyms1?>>> GetGyms(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<Gyms1?>>(false);
            try
            {
                if (id == -1)
                {
                    response.Data = await GetAllAsync();
                    response.Message = "Fetched all gyms successfully.";
                }
                else
                {
                    var gym = await GetByIdAsync(id);
                    response.Data = new List<Gyms1?> { gym };
                    response.Message = "Fetched gym successfully.";
                }
                response.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<Gyms1?>> GetGymById(int id)
        {
            var response = new ApiResponse<Gyms1?>(false);
            try
            {
                var gym = await GetByIdAsync(id);
                if (gym != null)
                {
                    response.Data = gym;
                    response.Success = true;
                    response.Message = "Fetched gym successfully.";
                }
                else
                {
                    response.Message = "Gym not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateGym(GymViewModel1 gym, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<Gyms1>(gym);
                //_dbContext.Gyms1.Add(entity);
                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;

                int id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Gym created successfully." : "Failed to create gym.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateGym(int id, GymViewModel1 gym, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _mapper.Map(gym, entity);
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Gym updated successfully." : "Failed to update gym.";
                }
                else
                {
                    response.Message = "Gym not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteGym(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    // Optionally set IsDeleted flag before physical delete depending on repository logic
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(id);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Gym deleted successfully." : "Failed to delete gym.";
                }
                else
                {
                    response.Message = "Gym not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
