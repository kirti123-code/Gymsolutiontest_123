using System;

namespace MODELS.Entities
{
    public class GymCloseReason : BaseEntity
    {
        public DateTime CloseDate { get; set; }
        public string Reason { get; set; }
        public string? Description { get; set; }
    }
}
