
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
using System.Linq;
using System.Threading.Tasks;

namespace SERVICES
{
    public class GymCloseDatesService : Repository<GymCloseDates>, IGymCloseDatesService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context; // ✅ Added to access Gym table

        public GymCloseDatesService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        // ✅ UPDATED METHOD — includes GymName join
        public async Task<ApiResponse<IEnumerable<object>>> GetGymCloseDates(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<object>>(false);

            try
            {
                if (id == -1)
                {
                    // ✅ Join GymCloseDates with Gyms1 to include GymName
                    var data = await _context.GymCloseDates
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.Gym) // assuming Gym navigation property exists
                        .Select(x => new
                        {
                            x.Id,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.CloseDate,
                            x.Reason,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        })
                        .ToListAsync();

                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all gym close dates successfully.";
                }
                else
                {
                    var closeDate = await _context.GymCloseDates
                        .Include(x => x.Gym)
                        .Where(x => x.Id == id && !x.IsDeleted)
                        .Select(x => new
                        {
                            x.Id,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.CloseDate,
                            x.Reason,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        })
                        .FirstOrDefaultAsync();

                    response.Data = new List<object> { closeDate };
                    response.Success = true;
                    response.Message = "Fetched gym close date successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<GymCloseDates?>> GetGymCloseDateById(int id)
        {
            var response = new ApiResponse<GymCloseDates?>(false);
            try
            {
                var closeDate = await GetByIdAsync(id);
                if (closeDate != null)
                {
                    response.Data = closeDate;
                    response.Success = true;
                    response.Message = "Fetched gym close date successfully.";
                }
                else
                {
                    response.Message = "Gym close date not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateGymCloseDate(GymCloseDatesViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var closeDate = _mapper.Map<GymCloseDates>(model);

                closeDate.CreatedBy = currentUsername;
                closeDate.CreatedOn = DateTime.UtcNow;

                int insertedId = await AddAsync(closeDate);

                if (insertedId > 0)
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Gym close date created successfully.";
                }
                else
                {
                    response.Data = false;
                    response.Message = "Failed to create gym close date.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateGymCloseDate(int id, GymCloseDatesViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);

            try
            {
                var closeDate = await GetByIdAsync(id);
                if (closeDate == null)
                {
                    response.Message = "Gym close date not found.";
                    return response;
                }

                _mapper.Map(model, closeDate);

                closeDate.UpdatedBy = currentUsername;
                closeDate.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(closeDate);

                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Gym close date updated successfully." : "Failed to update gym close date.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteGymCloseDate(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Gym close date deleted successfully." : "Failed to delete gym close date.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }
}
