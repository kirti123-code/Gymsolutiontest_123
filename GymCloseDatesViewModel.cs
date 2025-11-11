using System;
using System.ComponentModel.DataAnnotations;

namespace MODELS.ViewModels
{
    public class GymCloseDatesViewModel
    {
        public int Id { get; set; }

        public int? GymId { get; set; }

        [Required]
        public DateTime CloseDate { get; set; }

        [Required]
        public string Reason { get; set; }
       
        public string? GymName { get; set; }
    }
}
