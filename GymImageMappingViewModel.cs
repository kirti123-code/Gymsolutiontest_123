using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MODELS.ViewModels
{
    public class GymImageMappingViewModel
    {
        public int Id { get; set; }

        // Multiple image support (base64 strings and filenames)
        public List<string> Images { get; set; } = new();
        public List<string> ImagesName { get; set; } = new();

        [Required]
        public int GymId { get; set; }
    }
}
