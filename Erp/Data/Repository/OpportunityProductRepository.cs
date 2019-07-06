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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Erp.Repository
{
    public class OpportunityProductRepository : Repository<OpportunityProduct, DataDbContext>, IOpportunityProductRepository
    {
        public OpportunityProductRepository(IConfiguration config, ILogger<OpportunityProductRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<OpportunityProduct>> GetAllOpportunityProducts(string id, byte[] error)
        {
            List<OpportunityProduct> products = new List<OpportunityProduct>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Crm_Wrapper.GetAllOpportunityProducts(id, out ProductPtr, error, _ConnectionString);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    OpportunityProduct product = (OpportunityProduct)Marshal.PtrToStructure(current, typeof(OpportunityProduct));

                    current = (IntPtr)((long)current + Marshal.SizeOf(product));
                    products.Add(product);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return products;
        }
    }
}
