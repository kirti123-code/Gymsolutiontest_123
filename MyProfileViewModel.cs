using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class MyProfileViewModel
    {
        public string Name { get; set; } = null!;

        public string EmailId { get; set; } = null!;       

        public string MobileNo { get; set; } = null!;

        public DateTime RegistrationDate { get; set; }
    }
}
