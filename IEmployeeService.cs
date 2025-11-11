using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;

namespace SERVICES.Interfaces
{

        public interface IEmployeeService
        {
            Task<ApiResponse<IEnumerable<EmployeeData>>> GetEmployees(int id = -1);
            Task<ApiResponse<EmployeeData?>> GetEmployeeById(int id);
            Task<ApiResponse<bool>> CreateEmployee(EmployeeViewModel model, string currentUsername);
            Task<ApiResponse<bool>> UpdateEmployee(int id, EmployeeViewModel model, string currentUsername);
            Task<ApiResponse<bool>> DeleteEmployee(int id, string currentUsername);
        }
    }


