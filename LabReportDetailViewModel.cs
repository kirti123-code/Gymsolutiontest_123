using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class LabReportDetailViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = null!;
        public string TestName { get; set; } = null!;
        public DateTime TestDate { get; set; }
        public string Result { get; set; } = null!;
        public string? Remarks { get; set; }
    }
}
