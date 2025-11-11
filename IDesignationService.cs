using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IDesignationService
    {
        Task<ApiResponse<IEnumerable<DesignationData?>>> GetDesignations(int id = -1);
        Task<ApiResponse<DesignationData?>> GetDesignationById(int id);
        Task<ApiResponse<bool>> CreateDesignation(DesignationViewModel designation, string currentUsername);
        Task<ApiResponse<bool>> UpdateDesignation(int Id, DesignationViewModel designation, string currentUsername);
        Task<ApiResponse<bool>> DeleteDesignation(int Id, string currentUsername);
    }
}
