using System.ComponentModel.DataAnnotations.Schema;

namespace MODELS.Entities
{
    public class EmployeeData : BaseEntity
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? email { get; set; }
        public string contactNo { get; set; }
        public string city { get; set; }
        public string? address { get; set; }

        // Foreign key to Designation
        public int? DesignationId { get; set; }

        // Navigation property
        [ForeignKey("DesignationId")]
        public virtual DesignationData? Designation { get; set; }

        public string? Gender { get; set; }               // required (Male/Female/Other)
        public string? Proficiency { get; set; }


    }

}
