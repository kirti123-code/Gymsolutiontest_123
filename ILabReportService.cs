using MODELS.Entities;
using MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface ILabReportService
    {
        Task<ApiResponse<IEnumerable<LabReportDetail>>> GetReports(int id = -1);
        Task<ApiResponse<LabReportDetail?>> GetReportById(int id);
        Task<ApiResponse<bool>> CreateReport(LabReportDetailViewModel model, string currentUsername);
        Task<ApiResponse<bool>> UpdateReport(int id, LabReportDetailViewModel model, string currentUsername);
        Task<ApiResponse<bool>> DeleteReport(int id, string currentUsername);
    }
}
