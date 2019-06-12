using Erp.Data;
using Erp.Interfaces;
using Erp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class CustomerRepository : Repository<Customer, DataDbContext>, ICustomerRepository
    {
        public CustomerRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}
