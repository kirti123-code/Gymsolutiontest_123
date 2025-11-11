using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class BusBookingViewModel
    {
      
        public string PassengerName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string BusNumber { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime JourneyDate { get; set; }
        public string SeatNumber { get; set; }
        public string BookingStatus { get; set; }
    }
}
