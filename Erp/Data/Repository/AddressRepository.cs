using Erp.Interfaces;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ModulesWrappers;
using Erp.Data;
using Microsoft.AspNetCore.Identity;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Erp.Repository
{
    public class AddressRepository : Repository<Address, DataDbContext>, IAddressRepository
    {
        public AddressRepository(IConfiguration config, ILogger<Repository.Repository<Address, DataDbContext>> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<int> AddCustomerAddress(Address entity, byte[] error)
        {
            int status = 0;

            Address address = (Address)(object)entity;
            if (_ConnectionString != null)
                status = await Task.Run(() => Crm_Wrapper.AddCustomerAddress(address, error, _ConnectionString));

            return status;
        }
    }
}