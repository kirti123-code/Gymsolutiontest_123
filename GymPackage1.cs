using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODELS.Entities
{
    public class GymPackage1 : BaseEntity
    {
        public int GymId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public int DurationInMonths { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("GymId")]
        public virtual Gyms1? Gym { get; set; }
    }
}
