using AutoMapper;
using DAL;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using SERVICES.Repository;

namespace SERVICES
{
    public class LabReportService : Repository<LabReportDetail>, ILabReportService
    {
        private readonly IMapper _mapper;

        public LabReportService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<LabReportDetail>>> GetReports(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<LabReportDetail>>(false);
            try
            {
                var data = id == -1 ? await GetAllAsync() : new List<LabReportDetail> { await GetByIdAsync(id) };
                response.Data = data;
                response.Success = true;
                response.Message = "Reports fetched successfully.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<LabReportDetail?>> GetReportById(int id)
        {
            var response = new ApiResponse<LabReportDetail?>(false);
            try
            {
                var report = await GetByIdAsync(id);
                response.Data = report;
                response.Success = report != null;
                response.Message = report != null ? "Report found." : "Report not found.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateReport(LabReportDetailViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var report = _mapper.Map<LabReportDetail>(model);
                report.CreatedBy = currentUsername;
                report.CreatedOn = DateTime.UtcNow;

                var id = await AddAsync(report);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Created successfully." : "Creation failed.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateReport(int id, LabReportDetailViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var report = await GetByIdAsync(id);
                if (report == null)
                {
                    response.Message = "Report not found.";
                    return response;
                }

                _mapper.Map(model, report);
                report.UpdatedBy = currentUsername;
                report.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(report);
                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Updated successfully." : "Update failed.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteReport(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Deleted successfully." : "Deletion failed.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }
}
