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
using Erp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Erp.Repository
{
    public class OpportunityRepository : Repository<Opportunity, DataDbContext>, IOpportunityRepository
    {
        public OpportunityRepository(IConfiguration config, ILogger<OpportunityRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            
        }
    }
}

