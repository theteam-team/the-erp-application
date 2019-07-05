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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace Erp.Repository
{
    public class CustomerRepository : Repository<Customer, DataDbContext>, ICustomerRepository
    {
        private IConfiguration _config;
        private Emergency _common;

        public CustomerRepository(Emergency common , IConfiguration config, ILogger<CustomerRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
              : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _config = config;
            _common = common;
          
        }
      
        public async Task<List<Customer>> getCustomerById(string id, byte[] error)
        {
            Console.WriteLine("Hereeee");
            List<Customer> customers = new List<Customer>();
            IntPtr CustomerPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.getCustomerById(id, out CustomerPtr,  error, _ConnectionString);

                IntPtr current = CustomerPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Customer customer = (Customer)Marshal.PtrToStructure(current, typeof(Customer));
                    current = (IntPtr)((long)current + Marshal.SizeOf(customer));
                    customers.Add(customer);
                }
                Marshal.FreeCoTaskMem(CustomerPtr);
            });
            return customers;
        }

        public async Task<List<AOrder>> getCustomerOrders(string id, byte[] error)
        {
            List<AOrder> orders = new List<AOrder>();
            IntPtr OrderPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.getCustomerOrders(id, out OrderPtr, error, _ConnectionString);

                IntPtr current = OrderPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    AOrder order = (AOrder)Marshal.PtrToStructure(current, typeof(AOrder));
                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(OrderPtr);
            });
            return orders;
        }
        public async Task<List<Account>> getCustomerAccount(string id, byte[] error)
        {
            List<Account> accounts = new List<Account>();
            IntPtr AccountPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.getCustomerAccount(id, out AccountPtr, error, _ConnectionString);

                IntPtr current = AccountPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Account account = (Account)Marshal.PtrToStructure(current, typeof(Account));
                    current = (IntPtr)((long)current + Marshal.SizeOf(account));
                    accounts.Add(account);
                }
                Marshal.FreeCoTaskMem(AccountPtr);
            });
            return accounts;
        }

        public async Task<List<Out>> reporting(byte[] error)
        {
            List<Out> outs = new List<Out>();
            IntPtr OutPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.reporting(out OutPtr, error , _ConnectionString);

                IntPtr current = OutPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Out  an_out = (Out)Marshal.PtrToStructure(current, typeof(Out));
                    current = (IntPtr)((long)current + Marshal.SizeOf(an_out));
                    outs.Add(an_out);
                }
                Marshal.FreeCoTaskMem(OutPtr);
            });
            return outs;
        }

    }
}
