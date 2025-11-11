using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODELS.Entities
{

    [Table("Gyms1")]
    public class Gyms1 : BaseEntity
    {
        public readonly string GymName;

        //public int Id { get; set; } // if BaseEntity already contains Id, remove this
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public string OwnerMobile1 { get; set; }
        public string? OwnerMobile2 { get; set; }
        public DateTime OrganizedDate { get; set; }
        public int? TotalMembersJoined { get; set; }
        public int? TotalMembersDeleted { get; set; }
        public int? CurrentActiveMembers { get; set; }
        [Column("ModifiedBy")]
        public new string? UpdatedBy
        {
            get => base.UpdatedBy;
            set => base.UpdatedBy = value;
        }

        [Column("ModifiedOn")]
        public new DateTime? UpdatedOn
        {
            get => base.UpdatedOn;
            set => base.UpdatedOn = value;
        }


        // navigation collection (one Gym => many GymTimings1)
        public ICollection<GymTimings1> GymTimings { get; set; } = new List<GymTimings1>();


        // ✅ Reverse navigation (optional but good practice)
        public virtual ICollection<GymCloseDates>? GymCloseDates { get; set; }

        //public virtual ICollection<GymImageMapping> GymImageMapping { get; set; }

        public ICollection<GymImageMapping> GymImageMappings { get; set; } = new List<GymImageMapping>();





    }
}
