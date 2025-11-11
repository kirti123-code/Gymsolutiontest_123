using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Entities
{
    public class LabReportDetail : BaseEntity
    {
        public string PatientName { get; set; } = null!;
        public string TestName { get; set; } = null!;
        public DateTime TestDate { get; set; }
        public string Result { get; set; } = null!;
        public string? Remarks { get; set; }
    }

}
