using Erp.Data;
using Erp.Interfaces;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Erp.Repository
{
    public class ProcRequestRepo : Repository<ProcRequest, AccountDbContext> , IProcRequestRepo
    {
        private AccountDbContext _accountDbContext;

        public ProcRequestRepo(IConfiguration config, ILogger<ProcRequestRepo> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountDbContext = accountDbContext;
            
        }
       
        public override async Task<ProcRequest> GetById(object id)
        {
            string name = (string)id;
            var res = _accountDbContext.ProcRequests.Include(x => x.WorkflowParameters).Where(dt => dt.Name == name).FirstOrDefault();
            return res;



        }
    }
}
