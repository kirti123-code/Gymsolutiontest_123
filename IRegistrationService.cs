using MODELS.Entities;
using MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IRegistrationService
    {
        Task<ApiResponse<IEnumerable<Registration?>>> GetRegistrations(int id = -1);
        Task<ApiResponse<Registration?>> GetRegistrationById(int id);
        Task<ApiResponse<bool>> CreateRegistration(RegistrationViewModel registration, string currentUsername);
        Task<ApiResponse<bool>> UpdateRegistration(int registrationId, RegistrationViewModel registration, string currentUsername);
        Task<ApiResponse<bool>> DeleteRegistration(int registrationId, string currentUsername);
    }
}
