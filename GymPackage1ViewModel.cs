using System;
using System.ComponentModel.DataAnnotations;

namespace MODELS.ViewModels
{
    public class GymPackage1ViewModel
    {
        public int Id { get; set; }

        [Required]
        public int GymId { get; set; }

        [Required]
        public string PackageName { get; set; }

        [Required]
        public int DurationInMonths { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
