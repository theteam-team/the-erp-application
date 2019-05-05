using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Models;
using Erp.Interfaces;
using Erp.ViewModels.CRN_Tabels;
using Erp.Data;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class OpportunityRepository : Repository<Opportunities_product>, IOpportunityRepository
    {
        public OpportunityRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}

