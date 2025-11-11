using System;

namespace MODELS.ViewModels
{
    public class GymViewModel1
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public string OwnerMobile1 { get; set; }
        public string? OwnerMobile2 { get; set; }
        public DateTime OrganizedDate { get; set; }
        public int? TotalMembersJoined { get; set; }
        public int? TotalMembersDeleted { get; set; }
        public int? CurrentActiveMembers { get; set; }
      
    }
}
