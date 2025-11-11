
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODELS.Entities
{
    [Table("GymImageMapping")]
    public class GymImageMapping : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Gym")]
        public int GymId { get; set; }

        // base64-encoded image content
        public string? Images { get; set; }

        [MaxLength(50)]
        public string? ImagesName { get; set; }

        public virtual Gyms1? Gym { get; set; }
    }
}

