// SERVICES/Interfaces/IGymCloseReasonService.cs
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IGymCloseReasonService
    {
        Task<ApiResponse<IEnumerable<GymCloseReason>>> GetAllReasons();
        Task<ApiResponse<GymCloseReason>> GetReasonByDate(DateTime date);
        Task<ApiResponse<bool>> CreateReason(GymCloseReasonViewModel model, string username);
        Task<ApiResponse<bool>> UpdateReason(int id, GymCloseReasonViewModel model, string username);
        Task<ApiResponse<bool>> DeleteReason(int id, string username);
    }
}
