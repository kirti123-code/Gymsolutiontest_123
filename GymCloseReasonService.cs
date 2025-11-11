// SERVICES/GymCloseReasonService.cs
using AutoMapper;
using DAL;
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
    public class GymCloseReasonService : Repository<GymCloseReason>, IGymCloseReasonService
    {
        private readonly IMapper _mapper;

        public GymCloseReasonService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GymCloseReason>>> GetAllReasons()
        {
            var response = new ApiResponse<IEnumerable<GymCloseReason>>(false);
            try
            {
                var data = await GetAllAsync();
                response.Data = data;
                response.Success = true;
                response.Message = "Fetched all close reasons successfully.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<GymCloseReason>> GetReasonByDate(DateTime date)
        {
            var response = new ApiResponse<GymCloseReason>(false);
            try
            {
                var reason = (await GetAllAsync())
                    .FirstOrDefault(r => r.CloseDate.Date == date.Date);

                if (reason != null)
                {
                    response.Data = reason;
                    response.Success = true;
                    response.Message = "Reason found for the selected date.";
                }
                else
                {
                    response.Message = "No close reason found for the selected date.";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> CreateReason(GymCloseReasonViewModel model, string username)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<GymCloseReason>(model);
                entity.CreatedBy = username;
                entity.CreatedOn = DateTime.UtcNow;

                var id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Close reason created." : "Failed to create.";
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateReason(int id, GymCloseReasonViewModel model, string username)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _mapper.Map(model, entity);
                    entity.UpdatedBy = username;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Updated successfully." : "Update failed.";
                }
                else
                {
                    response.Message = "Record not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteReason(int id, string username)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    entity.UpdatedBy = username;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(id);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Deleted successfully." : "Failed to delete.";
                }
                else
                {
                    response.Message = "Record not found.";
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
