using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;
using Erp.Data;
using Erp.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class BmpParmRepo : Repository<BpmWorkflowParameters, AccountDbContext> , IBmpParmRepo
    {
        public BmpParmRepo(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}
