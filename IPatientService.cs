using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IPatientService
    {
        Task<ApiResponse<IEnumerable<PatientData?>>> GetPatientDetails(int id = -1);
        Task<ApiResponse<PatientData?>> GetPatientById(int id);
        Task<ApiResponse<bool>> CreatePatient(PatientViewModel patient, string currentUsername);
        Task<ApiResponse<bool>> UpdatePatient(int patientId, PatientViewModel patient, string currentUsername);
        Task<ApiResponse<bool>> DeletePatient(int patientId, string currentUsername);

    }
}



