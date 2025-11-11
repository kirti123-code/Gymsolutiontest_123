using AutoMapper;
using DAL;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using SERVICES.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVICES
{
    public class StudentService : Repository<StudentData>, IStudentService
    {
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<StudentData>>> GetStudents(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<StudentData>>(false);

            try
            {
                if (id == -1)
                {
                    var data = await GetAllAsync();
                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all students successfully.";
                }
                else
                {
                    var student = await GetByIdAsync(id);
                    response.Data = new List<StudentData> { student };
                    response.Success = true;
                    response.Message = "Fetched student successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<StudentData?>> GetStudentById(int id)
        {
            var response = new ApiResponse<StudentData?>(false);
            try
            {
                var student = await GetByIdAsync(id);
                if (student != null)
                {
                    response.Data = student;
                    response.Success = true;
                    response.Message = "Fetched student successfully.";
                }
                else
                {
                    response.Message = "Student not found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateStudent(StudentViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var student = _mapper.Map<StudentData>(model);

                student.CreatedBy = currentUsername;
                student.CreatedOn = DateTime.UtcNow;

                int insertedId = await AddAsync(student);

                if (insertedId > 0)
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Student created successfully.";
                }
                else
                {
                    response.Message = "Failed to create student.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateStudent(int id, StudentViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);

            try
            {
                var student = await GetByIdAsync(id);
                if (student == null)
                {
                    response.Message = "Student not found.";
                    return response;
                }

                _mapper.Map(model, student);
                student.UpdatedBy = currentUsername;
                student.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(student);

                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Student updated successfully." : "Failed to update student.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteStudent(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Student deleted successfully." : "Failed to delete student.";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }
}
