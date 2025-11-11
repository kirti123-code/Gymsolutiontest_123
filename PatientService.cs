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
    public class PatientService : Repository<PatientData>, IPatientService
    {
        private readonly IMapper _mapper;

        public PatientService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<PatientData?>>> GetPatientDetails(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<PatientData?>>(false);
            try
            {
                if (id == -1)
                {
                    response.Data = await GetAllAsync();
                    response.Message = "Fetched all patient details successfully.";
                }
                else
                {
                    var pat = await GetByIdAsync(id);
                    response.Data = new List<PatientData?> { pat };
                    response.Message = "Fetched patient details successfully.";
                }
                response.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<PatientData?>> GetPatientById(int id)
        {
            var response = new ApiResponse<PatientData?>(false);
            try
            {
                var pat = await GetByIdAsync(id);
                if (pat != null)
                {
                    response.Data = pat;
                    response.Success = true;
                    response.Message = "Fetched patient successfully.";
                }
                else
                {
                    response.Message = "Patient not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreatePatient(PatientViewModel patient, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<PatientData>(patient);
                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;

                int id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Patient created successfully." : "Failed to create patient.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdatePatient(int patientId, PatientViewModel patient, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(patientId);
                if (entity != null)
                {
                    _mapper.Map(patient, entity);
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Patient updated successfully." : "Failed to update patient.";
                }
                else
                {
                    response.Message = "Patient not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeletePatient(int patientId, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(patientId);
                if (entity != null)
                {
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(patientId);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Patient deleted successfully." : "Failed to delete patient.";
                }
                else
                {
                    response.Message = "Patient not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
