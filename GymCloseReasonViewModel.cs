using System;

namespace MODELS.ViewModels
{
    public class GymCloseReasonViewModel
    {
        public DateTime CloseDate { get; set; }
        public string Reason { get; set; }
        public string? Description { get; set; }
    }
}
