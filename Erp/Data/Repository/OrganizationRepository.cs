using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;
using Erp.Data;
using Erp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Erp.Repository
{
    public class OrganizationRepository : Repository<Organization, AccountDbContext>, IOrganizationRepository
    {
       

        public OrganizationRepository(IConfiguration config, ILogger<OrganizationRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
           
        }

        public async Task<Organization> OraganizationExist(string organizationName)
        {
            return (_accountdbContext.Organizations.Where(Org => Org.Name == organizationName).FirstOrDefault());
        }
    }
}
