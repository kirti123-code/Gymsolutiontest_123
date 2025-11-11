
using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Common;
using SERVICES.Interfaces;
using SERVICES.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SERVICES
{
    public class GymImageMappingService : Repository<GymImageMapping>, IGymImageMappingService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GymImageMappingService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<object>>> GetGymImageMappings(int gymId = -1)
        {
            var response = new ApiResponse<IEnumerable<object>>(false);
            try
            {
                if (gymId == -1)
                {
                    var data = await _context.GymImageMapping
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.Gym)
                        .Select(x => new
                        {
                            x.Id,
                            x.ImagesName,
                            x.Images,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        }).ToListAsync();

                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all gym image mappings successfully.";
                }
                else
                {
                    var data = await _context.GymImageMapping
                        .Where(x => x.GymId == gymId && !x.IsDeleted)
                        .Include(x => x.Gym)
                        .Select(x => new
                        {
                            x.Id,
                            x.ImagesName,
                            x.Images,
                            x.GymId,
                            GymName = x.Gym != null ? x.Gym.Name : string.Empty,
                            x.CreatedOn,
                            x.UpdatedOn,
                            x.IsDeleted
                        }).ToListAsync();

                    response.Data = data;
                    response.Success = true;
                    response.Message = $"Fetched gym image mappings for GymId {gymId} successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<GymImageMapping?>> GetGymImageMappingById(int id)
        {
            var response = new ApiResponse<GymImageMapping?>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    response.Data = entity;
                    response.Success = true;
                    response.Message = "Fetched gym image mapping successfully.";
                }
                else
                {
                    response.Message = "Gym image mapping not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }


        public async Task<ApiResponse<bool>> CreateGymImageMapping(GymImageMappingViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            if (model == null || model.GymId <= 0)
            {
                response.Message = "Invalid request data.";
                return response;
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var images = model.Images ?? new List<string>();
                    var imageNames = model.ImagesName ?? new List<string>();
                    var max = Math.Max(images.Count, imageNames.Count);

                    for (int i = 0; i < max; i++)
                    {
                        var entity = new GymImageMapping
                        {
                            GymId = model.GymId,
                            Images = i < images.Count ? images[i] : null,
                            ImagesName = i < imageNames.Count ? imageNames[i] : null,
                            CreatedBy = currentUsername,
                            CreatedOn = DateTime.UtcNow
                        };

                        await AddAsync(entity);
                    }

                    await transaction.CommitAsync();

                    response.Data = true;
                    response.Success = true;
                    response.Message = "Gym image mappings created successfully.";
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.Message = $"Error: {ex.Message}";
                    return response;
                }
            });
        }

        public async Task<ApiResponse<bool>> UpdateGymImageMapping(int gymId, GymImageMappingViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            if (gymId <= 0)
            {
                response.Message = "Invalid Gym ID.";
                return response;
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Remove existing mappings
                    var existingMappings = _context.GymImageMapping.Where(x => x.GymId == gymId && !x.IsDeleted);
                    _context.RemoveRange(existingMappings);
                    await _context.SaveChangesAsync();

                    // Add new mappings
                    var images = model.Images ?? new List<string>();
                    var imageNames = model.ImagesName ?? new List<string>();
                    var max = Math.Max(images.Count, imageNames.Count);

                    for (int i = 0; i < max; i++)
                    {
                        var entity = new GymImageMapping
                        {
                            GymId = gymId,
                            Images = i < images.Count ? images[i] : null,
                            ImagesName = i < imageNames.Count ? imageNames[i] : null,
                            CreatedBy = currentUsername,
                            CreatedOn = DateTime.UtcNow
                        };
                        _context.GymImageMapping.Add(entity);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    response.Data = true;
                    response.Success = true;
                    response.Message = "Gym image mappings updated successfully.";
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.Message = $"Error updating image mappings: {ex.Message}";
                    return response;
                }
            });
        }

public async Task<ApiResponse<bool>> DeleteGymImageMapping(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Gym image mapping deleted successfully." : "Failed to delete gym image mapping.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }
}