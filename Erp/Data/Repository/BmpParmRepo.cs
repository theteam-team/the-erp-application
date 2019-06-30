using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;
using Erp.Data;
using Erp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Erp.Repository
{
    public class BmpParmRepo : Repository<BpmWorkflowParameters, AccountDbContext> , IBmpParmRepo
    {
        public BmpParmRepo(IConfiguration config, ILogger<BmpParmRepo> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
              : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}
