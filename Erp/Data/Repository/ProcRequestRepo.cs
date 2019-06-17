﻿using Erp.Data;
using Erp.Interfaces;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Erp.Repository
{
    public class ProcRequestRepo : Repository<ProcRequest, AccountDbContext> , IProcRequestRepo
    {
        private AccountDbContext _accountDbContext;

        public ProcRequestRepo(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
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
