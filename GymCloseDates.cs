using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODELS.Entities
{
    public class GymCloseDates : BaseEntity
    {
        
        // Foreign key to Gym
        public int? GymId { get; set; }
        public DateTime CloseDate { get; set; }   // NOT NULL
         public string? Reason { get; set; }
         public string? GymName { get; set; }


        [ForeignKey("GymId")]
        public virtual Gyms1? Gym { get; set; }
    }
}
