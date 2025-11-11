using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MODELS.ViewModels
{
    public class GymDropdownDto
    {
        public int GymId { get; set; }
        public string GymName { get; set; } = string.Empty;
    }

    public class GymTimingsViewModel1
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Session { get; set; } = string.Empty;

        // Represent times as "HH:mm"
        [Required]
        public string OpenTime { get; set; } = string.Empty;

        [Required]
        public string CloseTime { get; set; } = string.Empty;

        public int? GymId { get; set; }
        public string? GymName { get; set; }

        public bool IsDeleted { get; set; }

        // optional: list of gyms for dropdown population (not persisted)
        public List<GymDropdownDto> Gyms { get; set; } = new List<GymDropdownDto>();
    }
}
