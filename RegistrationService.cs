
using AutoMapper;
using DAL;
using global::SERVICES.Interfaces;
using global::SERVICES.Repository;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using SERVICES.Interfaces;
using SERVICES.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using System.Text;
using System.Security.Cryptography;

namespace SERVICES
{
    public class RegistrationService : Repository<Registration>, IRegistrationService
    {
        private readonly IMapper _mapper;

        public RegistrationService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<Registration?>>> GetRegistrations(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<Registration?>>(false);
            try
            {
                if (id == -1)
                    response.Data = await GetAllAsync();
                else
                    response.Data = new List<Registration?> { await GetByIdAsync(id) };

                response.Success = true;
                response.Message = "Fetched registration(s) successfully.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<Registration?>> GetRegistrationById(int id)
        {
            var response = new ApiResponse<Registration?>(false);
            try
            {
                var reg = await GetByIdAsync(id);
                if (reg != null)
                {
                    response.Data = reg;
                    response.Success = true;
                    response.Message = "Fetched successfully.";
                }
                else
                    response.Message = "Not found.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateRegistration(RegistrationViewModel registration, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<Registration>(registration);
                entity.Password = CryptoHelper.Encrypt(registration.Password); // Hash password
                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;

                int id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Registration created successfully." : "Failed to create.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateRegistration(int registrationId, RegistrationViewModel registration, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(registrationId);
                if (entity != null)
                {
                    _mapper.Map(registration, entity);
                   
                    entity.Password = CryptoHelper.Encrypt(registration.Password);

                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Updated successfully." : "Update failed.";
                }
                else
                    response.Message = "Not found.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteRegistration(int registrationId, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(registrationId);
                if (entity != null)
                {
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(registrationId);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Deleted successfully." : "Failed to delete.";
                }
                else
                    response.Message = "Not found.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }

    public static class CryptoHelper
    {
        public static string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}


