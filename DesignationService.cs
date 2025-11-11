using AutoMapper;
using DAL;
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
    public class DesignationService : Repository<DesignationData>, IDesignationService
    {
        private readonly IMapper _mapper;

        public DesignationService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<DesignationData?>>> GetDesignations(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<DesignationData?>>(false);
            try
            {
                if (id == -1)
                {
                    response.Data = await GetAllAsync();
                    response.Message = "Fetched all designations successfully.";
                }
                else
                {
                    var desg = await GetByIdAsync(id);
                    response.Data = new List<DesignationData?> { desg };
                    response.Message = "Fetched designation successfully.";
                }
                response.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<DesignationData?>> GetDesignationById(int id)
        {
            var response = new ApiResponse<DesignationData?>(false);
            try
            {
                var desg = await GetByIdAsync(id);
                if (desg != null)
                {
                    response.Data = desg;
                    response.Success = true;
                    response.Message = "Fetched designation successfully.";
                }
                else
                {
                    response.Message = "Designation not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateDesignation(DesignationViewModel designation, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<DesignationData>(designation);
                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;

                int id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Designation created successfully." : "Failed to create designation.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateDesignation(int designationId, DesignationViewModel designation, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(designationId);
                if (entity != null)
                {
                    _mapper.Map(designation, entity);
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Designation updated successfully." : "Failed to update designation.";
                }
                else
                {
                    response.Message = "Designation not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteDesignation(int designationId, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(designationId);
                if (entity != null)
                {
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(designationId);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Designation deleted successfully." : "Failed to delete designation.";
                }
                else
                {
                    response.Message = "Designation not found.";
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
