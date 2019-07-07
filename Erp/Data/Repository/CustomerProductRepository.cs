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
    public class CustomerProductRepository : Repository<CustomerProduct, DataDbContext>, ICustomerProductRepository
    {
        private AccountDbContext _accountdbContext;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly DataDbContext _datadbContext;
        private Management _managment;

        public CustomerProductRepository(IConfiguration config, ILogger<CustomerProductRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountdbContext = accountDbContext;
            _usermanager = userManager;
            _datadbContext = datadbContext;
            _managment = management;

        }

        public async Task<List<CustomerProduct>> ShowCustomerProducts(string id, byte[] error)
        {
            List<CustomerProduct> customerProducts = new List<CustomerProduct>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {
                Console.WriteLine("showCustomerProducts c#");
                int number_fields = Warehouse_Wrapper.showCustomerProducts(id, out ProductPtr, error, _ConnectionString);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    CustomerProduct customerProduct = (CustomerProduct)Marshal.PtrToStructure(current, typeof(CustomerProduct));

                    current = (IntPtr)((long)current + Marshal.SizeOf(customerProduct));
                    customerProducts.Add(customerProduct);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return customerProducts;
        }
    }
}
