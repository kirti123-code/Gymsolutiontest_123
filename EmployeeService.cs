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
using System.Threading.Tasks;

namespace SERVICES
{
    public class EmployeeService : Repository<EmployeeData>, IEmployeeService
    {
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }





        public async Task<ApiResponse<IEnumerable<EmployeeData>>> GetEmployees(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<EmployeeData>>(false);

            try
            {
                if (id == -1)
                {
                    var data = await GetAllAsync();
                    response.Data = data;
                    response.Success = true;
                    response.Message = "Fetched all employees successfully.";
                }
                else
                {
                    var employee = await GetByIdAsync(id);
                    response.Data = new List<EmployeeData> { employee };
                    response.Success = true;
                    response.Message = "Fetched employee successfully.";
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<EmployeeData?>> GetEmployeeById(int id)
        {
            var response = new ApiResponse<EmployeeData?>(false);
            try
            {
                var employee = await GetByIdAsync(id);
                if (employee != null)
                {
                    response.Data = employee;
                    response.Success = true;
                    response.Message = "Fetched employee successfully.";
                }
                else
                {
                    response.Message = "Employee not found.";
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateEmployee(EmployeeViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var employee = _mapper.Map<EmployeeData>(model);

                // Set audit fields if you have them
                employee.CreatedBy = currentUsername;
                employee.CreatedOn = DateTime.UtcNow;

                // Use AddAsync from repository which returns inserted ID
                int insertedId = await AddAsync(employee);

                if (insertedId > 0)
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Employee created successfully.";
                }
                else
                {
                    response.Data = false;
                    response.Message = "Failed to create employee.";
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateEmployee(int id, EmployeeViewModel model, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);

            try
            {
                var employee = await GetByIdAsync(id);
                if (employee == null)
                {
                    response.Message = "Employee not found.";
                    return response;
                }

                _mapper.Map(model, employee);

                // employee.UpdatedBy = currentUsername;
                // employee.UpdatedOn = DateTime.UtcNow;

                var updated = await UpdateAsync(employee);

                response.Data = updated;
                response.Success = updated;
                response.Message = updated ? "Employee updated successfully." : "Failed to update employee.";
            }
            catch (Exception ex)
            {
                // log ex if needed
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteEmployee(int id, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var deleted = await DeleteAsync(id);
                response.Data = deleted;
                response.Success = deleted;
                response.Message = deleted ? "Employee deleted successfully." : "Failed to delete employee.";
            }
            catch (Exception ex)
            {
                // log ex if needed
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
    }
}
