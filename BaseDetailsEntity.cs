using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Entities
{
    public abstract class BaseDetailsEntity
    {
        public bool IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;


    }
}
