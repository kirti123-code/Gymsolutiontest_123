using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? email { get; set; }
        public string contactNo { get; set; }
        public string city { get; set; }
        public string? address { get; set; }

          public string Gender { get; set; }                 // Male/Female/Other
         public string Proficiency { get; set; }     // e.g. ["C#","Angular"]

        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }



    }
}

