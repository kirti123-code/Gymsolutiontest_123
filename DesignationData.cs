using System;

namespace MODELS.Entities
{
    public class DesignationData : BaseEntity
    {
        //public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<EmployeeData> Employees { get; set; } = new List<EmployeeData>();

    }
}
