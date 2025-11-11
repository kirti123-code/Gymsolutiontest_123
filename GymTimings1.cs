//using MODELS.Entities;

//public class GymTimings1 : BaseEntity
//{
//    public string Session { get; set; } = string.Empty;
//    public TimeSpan OpenTime { get; set; }
//    public TimeSpan CloseTime { get; set; }

//    // FK property
//    public int? GymId { get; set; }

//    // navigation to the Gym (single)
//    public Gyms1? Gym { get; set; }

//    // New column in table
//    public string? GymName { get; set; }
//    //public string? Gyms1 { get; set; }
//}
using System;

namespace MODELS.Entities
{
    public class GymTimings1 : BaseEntity
    {
        public string Session { get; set; } = string.Empty;
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        // Foreign key to Gyms1
        public int? GymId { get; set; }

        // Denormalized display name (optional)
        public string? GymName { get; set; }

        // Navigation property
        public Gyms1? Gym { get; set; }
    }
}
