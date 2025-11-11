using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;

namespace SERVICES.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<IEnumerable<StudentData>>> GetStudents(int id = -1);
        Task<ApiResponse<StudentData?>> GetStudentById(int id);
        Task<ApiResponse<bool>> CreateStudent(StudentViewModel model, string currentUsername);
        Task<ApiResponse<bool>> UpdateStudent(int id, StudentViewModel model, string currentUsername);
        Task<ApiResponse<bool>> DeleteStudent(int id, string currentUsername);
    }
}
