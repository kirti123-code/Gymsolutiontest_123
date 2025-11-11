using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
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
    public class GymPackage1Service : Repository<GymPackage1>, IGymPackage1Service
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GymPackage1Service(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<object>>> GetGymPackages(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<object>>(false);
            try
            {
                if (id == -1)
                {
                    var data = await _context.GymPackage1
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.Gym)
                        .Select(x => new
                        {
                            x.Id,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.PackageName,
                            x.DurationInMonths,
                            x.Price,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        }).ToListAsync();

                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all gym packages successfully.";
                }
                else
                {
                    var package = await _context.GymPackage1
                        .Include(x => x.Gym)
                        .Where(x => x.Id == id && !x.IsDeleted)
                        .Select(x => new
                        {
                            x.Id,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.PackageName,
                            x.DurationInMonths,
                            x.Price,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        }).FirstOrDefaultAsync();

                    response.Data = new List<object> { package };
                    response.Success = true;
                    response.Message = "Fetched gym package successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<GymPackage1?>> GetGymPackageById(int id)
        {
            var response = new ApiResponse<GymPackage1?>(false);
            try
            {
                var package = await GetByIdAsync(id);
                if (package != null)
                {
                    response.Data = package;
                    response.Success = true;
                    response.Message = "Fetched gym package successfully.";
                }
                else
                {
                    response.Message = "Gym package not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateGymPackage(GymPackage1ViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var package = _mapper.Map<GymPackage1>(model);
                package.CreatedBy = currentUsername;
                package.CreatedOn = DateTime.UtcNow;

                int insertedId = await AddAsync(package);

                response.Data = insertedId > 0;
                response.Success = insertedId > 0;
                response.Message = insertedId > 0 ? "Gym package created successfully." : "Failed to create gym package.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateGymPackage(int id, GymPackage1ViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var package = await GetByIdAsync(id);
                if (package == null)
                {
                    response.Message = "Gym package not found.";
                    return response;
                }

                _mapper.Map(model, package);
                package.UpdatedBy = currentUsername;
                package.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(package);
                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Gym package updated successfully." : "Failed to update gym package.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteGymPackage(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Gym package deleted successfully." : "Failed to delete gym package.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
    }
}
