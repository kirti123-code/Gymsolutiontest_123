using MODELS;
using DAL;
using SERVICES.Interfaces;
using SERVICES.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class RoleService : Repository<Role>, IRolesService
    {
        public RoleService(AppDbContext context) : base(context)

        {
        }
    }
}
