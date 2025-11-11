using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class PatientViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? email { get; set; }
        public string contactNo { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string? address { get; set; }
    }
}
