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
    public class BusBookingService : Repository<BusBookingData>, IBusBookingService
    {
        private readonly IMapper _mapper;

        public BusBookingService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<BusBookingData?>>> GetBusBookingDetails(int id = -1)
        {
            var response = new ApiResponse<IEnumerable<BusBookingData?>>(false);
            try
            {
                if (id == -1)
                {
                    response.Data = await GetAllAsync();
                    response.Message = "Fetched all bus bookings successfully.";
                }
                else
                {
                    var booking = await GetByIdAsync(id);
                    response.Data = new List<BusBookingData?> { booking };
                    response.Message = "Fetched bus booking successfully.";
                }
                response.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<BusBookingData?>> GetBusBookingById(int id)
        {
            var response = new ApiResponse<BusBookingData?>(false);
            try
            {
                var booking = await GetByIdAsync(id);
                if (booking != null)
                {
                    response.Data = booking;
                    response.Success = true;
                    response.Message = "Fetched booking successfully.";
                }
                else
                {
                    response.Message = "Booking not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> CreateBusBooking(BusBookingViewModel booking, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = _mapper.Map<BusBookingData>(booking);
                entity.CreatedBy = currentUsername;
                entity.CreatedOn = DateTime.UtcNow;

                int id = await AddAsync(entity);
                response.Data = id > 0;
                response.Success = id > 0;
                response.Message = id > 0 ? "Bus booking created successfully." : "Failed to create booking.";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> UpdateBusBooking(int bookingId, BusBookingViewModel booking, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(bookingId);
                if (entity != null)
                {
                    _mapper.Map(booking, entity);
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var updated = await UpdateAsync(entity);
                    response.Data = updated;
                    response.Success = updated;
                    response.Message = updated ? "Bus booking updated successfully." : "Failed to update booking.";
                }
                else
                {
                    response.Message = "Booking not found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteBusBooking(int bookingId, string currentUsername)
        {
            var response = new ApiResponse<bool>(false);
            try
            {
                var entity = await GetByIdAsync(bookingId);
                if (entity != null)
                {
                    entity.UpdatedBy = currentUsername;
                    entity.UpdatedOn = DateTime.UtcNow;

                    var deleted = await DeleteAsync(bookingId);
                    response.Data = deleted;
                    response.Success = deleted;
                    response.Message = deleted ? "Bus booking deleted successfully." : "Failed to delete booking.";
                }
                else
                {
                    response.Message = "Booking not found.";
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
