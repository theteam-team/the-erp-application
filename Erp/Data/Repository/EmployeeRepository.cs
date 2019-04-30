﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Models;
using Erp.Interfaces;
using Erp.Data;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, userManager)
        {

        }
    }
}
