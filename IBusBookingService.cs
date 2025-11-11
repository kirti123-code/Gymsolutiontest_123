using MODELS.Entities;
using MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.Interfaces
{
    public interface IBusBookingService
    {
        Task<ApiResponse<IEnumerable<BusBookingData?>>> GetBusBookingDetails(int id = -1);
        Task<ApiResponse<BusBookingData?>> GetBusBookingById(int id);
        Task<ApiResponse<bool>> CreateBusBooking(BusBookingViewModel booking, string currentUsername);
        Task<ApiResponse<bool>> UpdateBusBooking(int bookingId, BusBookingViewModel booking, string currentUsername);
        Task<ApiResponse<bool>> DeleteBusBooking(int bookingId, string currentUsername);
    }
}
