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
    public class GymTimingsService1 : Repository<GymTimings1>, IGymTimingsService1
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GymTimingsService1(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<GymTimingsDto>>> GetGymTimings(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<GymTimingsDto>>(false);
            try
            {
                var query = _context.GymTimings1
                    .Include(g => g.Gym)
                    .AsQueryable();

                if (id != -1) query = query.Where(x => x.Id == id && !x.IsDeleted);
                else query = query.Where(x => !x.IsDeleted);

                var data = await query
                    .Select(x => new GymTimingsDto
                    {
                        Id = x.Id,
                        Session = x.Session,
                        OpenTime = x.OpenTime.ToString(@"hh\:mm"),
                        CloseTime = x.CloseTime.ToString(@"hh\:mm"),
                        GymId = x.GymId,
                        GymName = x.Gym != null ? (x.Gym.Name ?? x.GymName) : x.GymName
                    })
                    .ToListAsync();

                response.Data = data;
                response.Success = true;
                response.Message = "Fetched gym timings successfully.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<GymTimings1?>> GetGymTimingById(int id)
        {
            var response = new ApiResponse<GymTimings1?>(false);
            try
            {
                var timing = await _context.GymTimings1
                    .Include(g => g.Gym)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (timing != null)
                {
                    response.Data = timing;
                    response.Success = true;
                    response.Message = "Fetched gym timing successfully.";
                }
                else
                {
                    response.Message = "Gym timing not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> CreateGymTiming(GymTimingsViewModel1 vm, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<GymTimings1>(vm);

                // parse times safely
                if (!TimeSpan.TryParse(vm.OpenTime, out var openTs)) openTs = TimeSpan.Zero;
                if (!TimeSpan.TryParse(vm.CloseTime, out var closeTs)) closeTs = TimeSpan.Zero;

                entity.OpenTime = openTs;
                entity.CloseTime = closeTs;

                // populate GymName from Gyms1 if available
                if (entity.GymId.HasValue)
                {
                    var gymName = await _context.Gyms1
                        .Where(g => g.Id == entity.GymId.Value)
                        .Select(g => g.Name)
                        .FirstOrDefaultAsync();

                    entity.GymName = gymName ?? vm.GymName;
                }
                else
                {
                    entity.GymName = vm.GymName;
                }

                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;
                entity.IsDeleted = false;

                int insertedId = await AddAsync(entity);
                response.Data = insertedId > 0;
                response.Success = insertedId > 0;
                response.Message = response.Success ? "Gym timing created successfully." : "Failed to create gym timing.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateGymTiming(int id, GymTimingsViewModel1 vm, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                {
                    response.Message = "Gym timing not found.";
                    return response;
                }

                // map simple fields
                entity.Session = vm.Session ?? entity.Session;

                if (!TimeSpan.TryParse(vm.OpenTime, out var openTs)) openTs = entity.OpenTime;
                if (!TimeSpan.TryParse(vm.CloseTime, out var closeTs)) closeTs = entity.CloseTime;

                entity.OpenTime = openTs;
                entity.CloseTime = closeTs;

                entity.GymId = vm.GymId;
                // refresh GymName from Gym table if possible
                if (entity.GymId.HasValue)
                {
                    var gymName = await _context.Gyms1
                        .Where(g => g.Id == entity.GymId.Value)
                        .Select(g => g.Name)
                        .FirstOrDefaultAsync();

                    entity.GymName = gymName ?? vm.GymName ?? entity.GymName;
                }
                else
                {
                    entity.GymName = vm.GymName ?? entity.GymName;
                }

                entity.UpdatedBy = currentUsername;
                entity.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(entity);
                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Gym timing updated successfully." : "Failed to update gym timing.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteGymTiming(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                // Soft-delete pattern: set IsDeleted if your repository supports it,
                // otherwise DeleteAsync will remove row. Here we call DeleteAsync to keep consistent.
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Gym timing deleted successfully." : "Failed to delete gym timing.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }

    // DTO used by Get methods
    public class GymTimingsDto
    {
        public int Id { get; set; }
        public string? Session { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public int? GymId { get; set; }
        public string? GymName { get; set; }
    }
}
